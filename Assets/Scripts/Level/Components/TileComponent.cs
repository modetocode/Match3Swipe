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
        [SerializeField]
        private Sprite amberSelectedSprite;
        [SerializeField]
        private Sprite emeraldSelectedSprite;
        [SerializeField]
        private Sprite prismSelectedSprite;
        [SerializeField]
        private Sprite rubySelectedSprite;
        [SerializeField]
        private Sprite sapphireSelectedSprite;

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
            this.SetTileSprite();
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
            this.spriteRenderer.sprite = this.Tile.IsInSelection ? this.GetSelectedTileSprite(this.Tile.TileType) : this.GetNotSelectedTileSprite(this.Tile.TileType);
        }

        private void SetTilePosition() {
            this.transform.position = this.Tile.Position;
        }

        private Sprite GetNotSelectedTileSprite(TileType tileType) {
            switch (this.Tile.TileType) {
                case TileType.Amber:
                    return amberSprite;
                case TileType.Emerald:
                    return emeraldSprite;
                case TileType.Prism:
                    return prismSprite;
                case TileType.Ruby:
                    return rubySprite;
                case TileType.Sapphire:
                    return sapphireSprite;
                default:
                    throw new InvalidOperationException("Not supported tile type");
            }
        }

        private Sprite GetSelectedTileSprite(TileType tileType) {
            switch (this.Tile.TileType) {
                case TileType.Amber:
                    return amberSelectedSprite;
                case TileType.Emerald:
                    return emeraldSelectedSprite;
                case TileType.Prism:
                    return prismSelectedSprite;
                case TileType.Ruby:
                    return rubySelectedSprite;
                case TileType.Sapphire:
                    return sapphireSelectedSprite;
                default:
                    throw new InvalidOperationException("Not supported tile type");
            }
        }
    }
}
