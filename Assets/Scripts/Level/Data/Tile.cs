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

        public Tile(TileType tileType) {
            this.TileType = tileType;
        }

        public void DeleteTile() {
            if (this.TileRemoved != null) {
                this.TileRemoved();
            }
        }
    }
}
