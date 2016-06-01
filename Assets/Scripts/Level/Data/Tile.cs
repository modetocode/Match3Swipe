using UnityEngine;

namespace Modetocode.Swiper.Level.Data {

    /// <summary>
    /// Represents one tile in a match3 game.
    /// </summary>
    public class Tile : IPositionableObject {

        public TileType TileType { get; private set; }
        public Vector2 Position { get; set; }

        public Tile(TileType tileType) {
            this.TileType = tileType;
        }
    }
}
