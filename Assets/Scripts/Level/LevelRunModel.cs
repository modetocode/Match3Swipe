using Modetocode.Swiper.Level.Data;
using Modetocode.Swiper.Level.Util;
using System;

namespace Modetocode.Swiper.Level {

    /// <summary>
    /// Stores the data of one level
    /// </summary>
    public class LevelRunModel {

        public GameBoard GameBoard { get; private set; }
        public TileSelection TileSelection { get; private set; }
        public float Score { get; private set; }
        public float RemainingTimeInSeconds { get { return this.LevelRunTimeManager.RemainingTimeInSeconds; } }

        private LevelRunTimeManager LevelRunTimeManager { get; set; }

        public LevelRunModel(GameBoard gameBoard, LevelRunTimeManager levelRunTimeManager) {
            if (gameBoard == null) {
                throw new ArgumentNullException("gameBoard");
            }

            if (levelRunTimeManager == null) {
                throw new ArgumentNullException("levelRunTimeManager");
            }

            this.GameBoard = gameBoard;
            this.LevelRunTimeManager = levelRunTimeManager;
            this.TileSelection = new TileSelection();
            this.Score = 0f;

        }

        public void AddScore(float score) {
            if (score < 0) {
                throw new ArgumentOutOfRangeException("score");
            }

            this.Score += score;
        }

        public void ResetScore() {
            this.Score = 0f;
        }
    }
}
