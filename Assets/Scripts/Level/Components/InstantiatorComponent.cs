using Modetocode.Swiper.Level.Data;
using System;
using UnityEngine;

namespace Modetocode.Swiper.Level.Components {

    /// <summary>
    /// Responsible for instantiating all of the objects on the scene that will be added on runtime
    /// </summary>
    public class InstantiatorComponent : MonoBehaviour {

        [SerializeField]
        private TileComponent tileComponentTemplate;

        public void InstantiateTile(Tile newTile) {
            if (newTile == null) {
                throw new ArgumentNullException("newTile");
            }

            //TODO add object pool
            TileComponent newTileComponent = Instantiate(tileComponentTemplate);
            newTileComponent.Initialize(newTile);
        }
    }
}
