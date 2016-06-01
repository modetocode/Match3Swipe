using Modetocode.Swiper.Level.Data;
using System.Collections.Generic;

namespace Modetocode.Swiper.Util {

    /// <summary>
    /// Contains methods that are connected with game board.
    /// </summary>
    public static class GameBoardManager {
        public static GameBoard CreateGameBoard(int rowCount, int columnCount, IList<TileType> availableTileTypes) {
            //TODO arg check
            return new GameBoard(rowCount, columnCount, availableTileTypes);
        }
    }
}