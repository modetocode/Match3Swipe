using Modetocode.Swiper.Level.Data;
using Modetocode.Swiper.Util;
using System;
using UnityEngine;

namespace Modetocode.Swiper.Level.Components {

    /// <summary>
    /// Responsible for instantiating all of the objects on the scene that will be added on runtime
    /// </summary>
    public class InstantiatorComponent : MonoBehaviour {

        [SerializeField]
        private TileComponent tileComponentTemplate;

        private SimplePool<TileComponent> tilesPool;

        public void InstantiateTile(Tile newTile) {
            if (newTile == null) {
                throw new ArgumentNullException("newTile");
            }

            if (this.tilesPool == null) {
                Func<TileComponent> tileComponentFactoryFunc = () => {
                    TileComponent instantiatedObject = Instantiate(tileComponentTemplate);
                    instantiatedObject.gameObject.SetActive(false);
                    return instantiatedObject;
                };

                this.tilesPool = new SimplePool<TileComponent>(tileComponentFactoryFunc, 0);
            }

            TileComponent newTileComponent = this.tilesPool.Fetch();
            newTileComponent.Initialize(newTile, this.OnTileComponentDestroyedAction);
            newTileComponent.gameObject.SetActive(true);
        }

        private void OnTileComponentDestroyedAction(TileComponent tileComponent) {
            tileComponent.gameObject.SetActive(false);
            this.tilesPool.Release(tileComponent);
        }
    }
}
