using System;
using System.Collections.Generic;

namespace Modetocode.Swiper.Level.Data {

    /// <summary>
    /// Represents the game board of a match3 game.
    /// </summary>
    public class GameBoard {

        public event Action<Tile> TileAdded;

        public int ColumnCount { get; private set; }
        public int RowCount { get; private set; }
        public IList<TileType> AvailableTileTypes { get; private set; }
        public IList<IList<Tile>> Tiles { get; private set; }

        public GameBoard(int rowCount, int columnCount, IList<TileType> availableTileTypes) {
            if (rowCount <= 0) {
                throw new ArgumentOutOfRangeException("rowCount", "Cannot be zero or less.");
            }

            if (columnCount <= 0) {
                throw new ArgumentOutOfRangeException("columnCount", "Cannot be zero or less.");
            }

            if (availableTileTypes == null) {
                throw new ArgumentNullException("availableTileTypes");
            }

            this.RowCount = rowCount;
            this.ColumnCount = columnCount;
            this.AvailableTileTypes = availableTileTypes;
            this.Tiles = new Tile[this.RowCount][];
            for (int i = 0; i < this.RowCount; i++) {
                this.Tiles[i] = new Tile[this.ColumnCount];
            }
        }

        public bool HasTileForSlot(Slot slot) {
            if (slot == null) {
                throw new ArgumentNullException("slot");
            }

            return this.Tiles[slot.RowIndex][slot.ColumnIndex] != null;
        }

        public void AddTile(Tile tile) {
            if (tile == null) {
                throw new ArgumentNullException("tile");
            }

            this.Tiles[tile.AssignedSlot.RowIndex][tile.AssignedSlot.ColumnIndex] = tile;
            if (this.TileAdded != null) {
                this.TileAdded(tile);
            }
        }

        public void RemoveTileFromSlot(Slot slot, bool shouldDeleteTile) {
            if (slot == null) {
                throw new ArgumentNullException("slot");
            }

            Tile removedTile = this.Tiles[slot.RowIndex][slot.ColumnIndex];
            if (removedTile == null) {
                return;
            }

            this.Tiles[slot.RowIndex][slot.ColumnIndex] = null;
            if (shouldDeleteTile) {
                removedTile.DeleteTile();
            }
        }
    }
}
