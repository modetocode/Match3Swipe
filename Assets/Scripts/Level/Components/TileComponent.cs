using Modetocode.Swiper.Level.Data;
using System;
using UnityEngine;

namespace Modetocode.Swiper.Level.Components {

    /// <summary>
    /// Visual representation of a tile.
    /// </summary>
    public class TileComponent : MonoBehaviour {

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Sprite amberSprite;
        [SerializeField]
        private Sprite emeraldSprite;
        [SerializeField]
        private Sprite prismSprite;
        [SerializeField]
        private Sprite rubySprite;
        [SerializeField]
        private Sprite sapphireSprite;

        public Tile Tile { get; private set; }

        public void Initialize(Tile tile) {
            if (tile == null) {
                throw new ArgumentNullException("tile");
            }

            this.Tile = tile;
            this.SetTileSprite();
        }

        public void Update() {
            if (this.Tile == null) {
                return;
            }

            this.transform.position = this.Tile.Position;
        }

        private void SetTileSprite() {
            Sprite sprite;

            switch (this.Tile.TileType) {
                case TileType.Amber:
                    sprite = amberSprite;
                    break;
                case TileType.Emerald:
                    sprite = emeraldSprite;
                    break;
                case TileType.Prism:
                    sprite = prismSprite;
                    break;
                case TileType.Ruby:
                    sprite = rubySprite;
                    break;
                case TileType.Sapphire:
                    sprite = sapphireSprite;
                    break;
                default:
                    throw new InvalidOperationException("Not supported tile type");
            }

            this.spriteRenderer.sprite = sprite;
        }
    }
}
