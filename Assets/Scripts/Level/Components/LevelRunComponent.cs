using Modetocode.Swiper.Level.Data;
using UnityEngine;

namespace Modetocode.Swiper.Level.Components {

    /// <summary>
    /// Responsible for showing the logic on the screen.
    /// </summary>
    public class LevelRunComponent : MonoBehaviour {

        [SerializeField]
        private InstantiatorComponent instantiatorComponent;

        private LevelRunManager LevelRunManager { get; set; }

        public void Start() {
            this.LevelRunManager = new LevelRunManager();
            this.LevelRunManager.LevelRunModel.GameBoard.TileAdded += InstantiateTile;
            this.LevelRunManager.Start();
        }

        private void InstantiateTile(Tile newTile) {
            this.instantiatorComponent.InstantiateTile(newTile);
        }

        public void Update() {
            this.LevelRunManager.Tick(Time.deltaTime);
        }

        public void Destroy() {
            this.LevelRunManager.LevelRunModel.GameBoard.TileAdded -= InstantiateTile;
        }
    }
}