using System.Collections.Generic;

namespace Modetocode.Swiper.Level.Data {

    /// <summary>
    /// Represents the game board of a match3 game.
    /// </summary>
    public class GameBoard {

        private int ColumnCount { get; set; }
        private int RowCount { get; set; }
        private IList<TileType> AvailableTileTypes { get; set; }
        private IList<IList<Tile>> Tiles { get; set; }

        public GameBoard(int rowCount, int columnCount, IList<TileType> availableTileTypes) {
            //TODO arg check
            this.RowCount = rowCount;
            this.ColumnCount = columnCount;
            this.AvailableTileTypes = availableTileTypes;
            this.Tiles = new Tile[this.RowCount][];
            for (int i = 0; i < this.ColumnCount; i++) {
                this.Tiles[i] = new Tile[this.ColumnCount];
            }
        }
    }
}
