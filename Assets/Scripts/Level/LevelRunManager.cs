using Modetocode.Swiper.Animations;
using Modetocode.Swiper.Level.Data;
using Modetocode.Swiper.Level.Util;
using Modetocode.Swiper.Util;
using System;
using System.Collections.Generic;

namespace Modetocode.Swiper.Level {

    /// <summary>
    /// Responsible for execution of the logic of one level of the game. 
    /// </summary>
    public class LevelRunManager : ITickable {

        public event Action LevelFinished;

        private Ticker Ticker { get; set; }
        public LevelRunModel LevelRunModel { get; private set; }
        private NoMoreMovesDetector NoMoreMovesDetector { get; set; }
        private LevelRunTimeManager levelRunTimeManager { get; set; }

        public LevelRunManager() {
            //TODO give the appropriate data for creation of game board
            GameBoard gameBoard = GameBoardManager.CreateGameBoard(rowCount: 7, columnCount: 7, availableTileTypes: new TileType[] { TileType.Amber, TileType.Emerald, TileType.Prism, TileType.Ruby, TileType.Sapphire });
            this.levelRunTimeManager = new LevelRunTimeManager();
            this.LevelRunModel = new LevelRunModel(gameBoard, this.levelRunTimeManager);
            this.NoMoreMovesDetector = new NoMoreMovesDetector(gameBoard);
            this.Ticker = new Ticker(new ITickable[] { ObjectAnimator.Instance, this.NoMoreMovesDetector, this.levelRunTimeManager });
        }

        public void Start() {
            this.levelRunTimeManager.TimePassed += FinishRun;
            GameBoardManager.FillAndAnimateBoard(this.LevelRunModel.GameBoard);
        }

        public void Tick(float deltaTime) {
            this.Ticker.Tick(deltaTime);
        }

        public void ProcessTileForSelection(Tile tile) {
            if (tile == null) {
                throw new ArgumentNullException("tile");
            }

            TileSelection selection = this.LevelRunModel.TileSelection;
            selection.ProcessNextTileForSelection(tile);
        }

        public void FinishSelection() {
            TileSelection selection = this.LevelRunModel.TileSelection;
            IList<Tile> tilesToBeRemoved = selection.FinishSelection();
            GameBoard gameBoard = this.LevelRunModel.GameBoard;
            float score = ScoreCalculator.CalculateScore(tilesToBeRemoved);
            this.LevelRunModel.AddScore(score);
            float bonusTime = TimeBonusCalculator.CalculateBonusTime(tilesToBeRemoved);
            this.levelRunTimeManager.AddTime(bonusTime);
            GameBoardManager.RemoveTilesFromBoard(gameBoard, tilesToBeRemoved);
            GameBoardManager.FillAndAnimateBoard(gameBoard);
        }

        public void OnTickingFinished() {
            this.levelRunTimeManager.TimePassed -= FinishRun;
        }

        private void FinishRun() {
            this.Ticker.FinishTicking();
            if (this.LevelFinished != null) {
                this.LevelFinished();
            }
        }

        public void ResetData() {
            GameBoard gameBoard = this.LevelRunModel.GameBoard;
            GameBoardManager.RemoveAllTilesFromBoard(gameBoard);
            this.LevelRunModel.TileSelection.ClearSelection();
            this.levelRunTimeManager.Reset();
            this.Ticker.Reset();
            this.LevelRunModel.ResetScore();
        }
    }
}