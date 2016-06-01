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
        private IList<Tile> UnassignedTiles { get; set; }

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
            for (int i = 0; i < this.ColumnCount; i++) {
                this.Tiles[i] = new Tile[this.ColumnCount];
            }

            this.UnassignedTiles = new List<Tile>();
        }

        public bool HasTileForSlot(Slot slot) {
            if (slot == null) {
                throw new ArgumentNullException("slot");
            }

            //TODO there are also tiles that are still not set on the slot
            return this.Tiles[slot.RowIndex][slot.ColumnIndex] != null;
        }

        public void AddTile(Tile tile) {
            if (tile == null) {
                throw new ArgumentNullException("tile");
            }

            this.UnassignedTiles.Add(tile);
            if (this.TileAdded != null) {
                this.TileAdded(tile);
            }
        }

        public void AssignTileToSlot(Tile tile, Slot slot) {
            if (tile == null) {
                throw new ArgumentNullException("tile");
            }

            if (slot == null) {
                throw new ArgumentNullException("slot");
            }

            if (!this.UnassignedTiles.Contains(tile)) {
                throw new InvalidOperationException("The tile wasn't previously added");
            }

            this.Tiles[slot.RowIndex][slot.ColumnIndex] = tile;
        }

        public void UnassignTileFromSlot(Slot slot) {
            if (slot == null) {
                throw new ArgumentNullException("slot");
            }

            Tile unassignedTile = this.Tiles[slot.RowIndex][slot.ColumnIndex];
            this.Tiles[slot.RowIndex][slot.ColumnIndex] = null;
            this.UnassignedTiles.Add(unassignedTile);
        }

        public void RemoveTileFromSlot(Slot slot) {
            if (slot == null) {
                throw new ArgumentNullException("slot");
            }

            Tile removedTile = this.Tiles[slot.RowIndex][slot.ColumnIndex];
            this.Tiles[slot.RowIndex][slot.ColumnIndex] = null;
            removedTile.DeleteTile();
        }

        public Slot GetSlotForTile(Tile tile) {
            if (tile == null) {
                throw new ArgumentNullException("tile");
            }

            for (int i = 0; i < this.RowCount; i++) {
                for (int j = 0; j < this.ColumnCount; j++) {
                    if (this.Tiles[i][j] == null) {
                        continue;
                    }

                    if (this.Tiles[i][j].Equals(tile)) {
                        return new Slot(i, j);
                    }
                }
            }

            return null;
        }
    }
}
