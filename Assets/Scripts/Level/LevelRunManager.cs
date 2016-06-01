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

        //TODO remove this when input is done and matches can be made - this is for testing purposes
        int numberOfTicks = 0;

        public void Tick(float deltaTime) {
            this.Ticker.Tick(deltaTime);

            //TODO remove this when input is done and matches can be made - this is for testing purposes 
            numberOfTicks++;
            if (numberOfTicks == 100) {
                this.LevelRunModel.GameBoard.RemoveTileFromSlot(new Slot(2, 2));
                this.LevelRunModel.GameBoard.RemoveTileFromSlot(new Slot(3, 2));
                this.LevelRunModel.GameBoard.RemoveTileFromSlot(new Slot(5, 2));
                this.LevelRunModel.GameBoard.RemoveTileFromSlot(new Slot(5, 3));
                for (int j = 0; j < this.LevelRunModel.GameBoard.ColumnCount; j++) {
                    this.LevelRunModel.GameBoard.RemoveTileFromSlot(new Slot(j, 0));
                }

                GameBoardManager.FillAndAnimateBoard(this.LevelRunModel.GameBoard);
            }
        }

        public void OnTickingFinished() {
        }
    }
}