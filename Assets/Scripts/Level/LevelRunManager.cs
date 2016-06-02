using Modetocode.Swiper.Animations;
using Modetocode.Swiper.Level.Data;
using Modetocode.Swiper.Util;
using System;
using System.Collections.Generic;

namespace Modetocode.Swiper.Level {

    /// <summary>
    /// Responsible for execution of the logic of one level of the game. 
    /// </summary>
    public class LevelRunManager : ITickable {

        private Ticker Ticker { get; set; }
        public LevelRunModel LevelRunModel { get; private set; }
        private NoMoreMovesDetector NoMoreMovesDetector { get; set; }

        public LevelRunManager() {
            //TODO give the appropriate data for creation of game board
            GameBoard gameBoard = GameBoardManager.CreateGameBoard(rowCount: 7, columnCount: 7, availableTileTypes: new TileType[] { TileType.Amber, TileType.Emerald, TileType.Prism, TileType.Ruby, TileType.Sapphire });
            this.LevelRunModel = new LevelRunModel(gameBoard);
            this.NoMoreMovesDetector = new NoMoreMovesDetector(gameBoard);
            this.Ticker = new Ticker(new ITickable[] { ObjectAnimator.Instance, this.NoMoreMovesDetector });

        }

        public void Start() {
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
            GameBoardManager.RemoveTilesFromBoard(gameBoard, tilesToBeRemoved);
            GameBoardManager.FillAndAnimateBoard(gameBoard);
        }

        public void OnTickingFinished() {
        }
    }
}