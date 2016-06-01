using Modetocode.Swiper.Animations;
using Modetocode.Swiper.Level.Data;
using Modetocode.Swiper.Util;

namespace Modetocode.Swiper.Level {

    /// <summary>
    /// Responsible for execution of the logic of one level of the game. 
    /// </summary>
    public class LevelRunManager : ITickable {

        private Ticker Ticker { get; set; }
        public LevelRunModel LevelRunModel { get; private set; }

        public LevelRunManager() {
            //TODO give the appropriate data for creation of game board
            GameBoard gameBoard = GameBoardManager.CreateGameBoard(rowCount: 7, columnCount: 7, availableTileTypes: new TileType[] { TileType.Amber, TileType.Emerald, TileType.Prism });
            this.LevelRunModel = new LevelRunModel(gameBoard);
            this.Ticker = new Ticker(new ITickable[] { ObjectAnimator.Instance });

        }

        public void Start() {
            GameBoardManager.FillAndAnimateBoard(this.LevelRunModel.GameBoard);
        }

        public void Tick(float deltaTime) {
            this.Ticker.Tick(deltaTime);
        }

        public void OnTickingFinished() {
        }
    }
}