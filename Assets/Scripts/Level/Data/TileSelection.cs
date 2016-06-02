using System;
using System.Collections.Generic;

namespace Modetocode.Swiper.Level.Data {

    /// <summary>
    /// Represents a sequence of tiles that the player currently have selected.
    /// </summary>
    public class TileSelection {

        public event Action<Tile> TileAdded;
        public event Action SelectionShortened;
        public event Action SelectionFinished;

        public IList<Tile> TileSequence { get; private set; }

        public TileSelection() {
            this.TileSequence = new List<Tile>();
        }

        public void ProcessNextTileForSelection(Tile tile) {
            if (tile == null) {
                throw new ArgumentNullException("tile");
            }

            if (this.TileSequence.Count == 0) {
                this.AddTileToSequence(tile);
                return;
            }

            if (this.TileSequence.Contains(tile)) {
                this.ShortenSequence(tile);
                return;
            }

            if (this.CanBeAddedToTheSequence(tile)) {
                this.AddTileToSequence(tile);
            }
        }

        private void AddTileToSequence(Tile tile) {
            this.TileSequence.Add(tile);
            tile.IsInSelection = true;
            if (this.TileAdded != null) {
                this.TileAdded(tile);
            }
        }

        private void ShortenSequence(Tile tile) {
            int index = this.TileSequence.IndexOf(tile);
            for (int i = this.TileSequence.Count - 1; i > index; i--) {
                this.TileSequence[i].IsInSelection = false;
                this.TileSequence.RemoveAt(i);
            }

            if (this.SelectionShortened != null) {
                this.SelectionShortened();
            }
        }

        private bool CanBeAddedToTheSequence(Tile tile) {
            Tile lastAddedTile = this.TileSequence[this.TileSequence.Count - 1];
            if (lastAddedTile.TileType != tile.TileType) {
                return false;
            }

            Slot lastAddedTileSlot = lastAddedTile.AssignedSlot;
            Slot newTileSlot = tile.AssignedSlot;
            int rowDifference = Math.Abs(lastAddedTileSlot.RowIndex - newTileSlot.RowIndex);
            int columnDifference = Math.Abs(lastAddedTileSlot.ColumnIndex - newTileSlot.ColumnIndex);
            return !(rowDifference > 1 || columnDifference > 1);
        }

        public IList<Tile> FinishSelection() {
            IList<Tile> successfullSequence = (TileSequence.Count <= 2) ? new List<Tile>() : this.TileSequence;
            for (int i = 0; i < this.TileSequence.Count; i++) {
                this.TileSequence[i].IsInSelection = false;
            }

            this.TileSequence = new List<Tile>();
            if (this.SelectionFinished != null) {
                this.SelectionFinished();
            }

            return successfullSequence;
        }
    }
}
