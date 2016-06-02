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
        private Action<TileComponent> OnTileDestroyedAction { get; set; }

        public void Initialize(Tile tile, Action<TileComponent> onTileDestroyedAction) {
            if (tile == null) {
                throw new ArgumentNullException("tile");
            }

            if (onTileDestroyedAction == null) {
                throw new ArgumentNullException("onTileDestroyedAction");
            }

            this.Tile = tile;
            this.OnTileDestroyedAction = onTileDestroyedAction;
            this.Tile.TileRemoved += DestroyComponent;
            this.SetTilePosition();
            this.SetTileSprite();
        }

        public void Update() {
            if (this.Tile == null) {
                return;
            }

            this.SetTilePosition();
        }

        public void Destroy() {
            if (this.Tile == null) {
                return;
            }

            this.DestroyComponent();
        }

        private void DestroyComponent() {
            this.Tile.TileRemoved -= DestroyComponent;
            this.OnTileDestroyedAction(this);
            this.Tile = null;
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

        private void SetTilePosition() {
            this.transform.position = this.Tile.Position;
        }
    }
}
