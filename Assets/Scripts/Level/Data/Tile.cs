using System;
using UnityEngine;

namespace Modetocode.Swiper.Level.Data {

    /// <summary>
    /// Represents one tile in a match3 game.
    /// </summary>
    public class Tile : IPositionableObject {

        public event Action TileRemoved;

        public TileType TileType { get; private set; }
        public Vector2 Position { get; set; }
        public Slot AssignedSlot { get; set; }
        public bool IsInSelection { get; set; }

        public Tile(TileType tileType, Slot assignedSlot) {
            if (assignedSlot == null) {
                throw new ArgumentNullException("assignedSlot");
            }

            this.TileType = tileType;
            this.AssignedSlot = assignedSlot;
            this.IsInSelection = false;
        }

        public void DeleteTile() {
            if (this.TileRemoved != null) {
                this.TileRemoved();
            }
        }
    }
}
