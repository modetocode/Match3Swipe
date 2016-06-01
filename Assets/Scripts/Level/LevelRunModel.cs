using Modetocode.Swiper.Level.Data;

namespace Modetocode.Swiper.Level {

    /// <summary>
    /// Stores the data of one level
    /// </summary>
    public class LevelRunModel {

        public GameBoard GameBoard { get; private set; }

        public LevelRunModel(GameBoard gameBoard) {
            //TODO arg check
            this.GameBoard = gameBoard;
        }
    }
}
