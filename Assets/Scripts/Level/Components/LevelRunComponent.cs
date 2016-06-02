using Modetocode.Swiper.Level.Data;
using UnityEngine;

namespace Modetocode.Swiper.Level.Components {

    /// <summary>
    /// Responsible for showing the logic on the screen.
    /// </summary>
    public class LevelRunComponent : MonoBehaviour {

        [SerializeField]
        private InstantiatorComponent instantiatorComponent;
        [SerializeField]
        private InputComponent inputComponent;
        [SerializeField]
        private TileSelectionComponent tileSelectionComponent;
        [SerializeField]
        private LevelRunCameraComponent levelRunCameraComponent;

        private LevelRunManager LevelRunManager { get; set; }
        private bool touchInProgress { get; set; }

        public void Start() {
            this.LevelRunManager = new LevelRunManager();
            this.LevelRunManager.LevelRunModel.GameBoard.TileAdded += InstantiateTile;
            this.touchInProgress = false;
            this.LevelRunManager.Start();
            this.inputComponent.TouchStarted += StartTouch;
            this.inputComponent.TouchEnded += EndTouch;
            this.inputComponent.TouchHit += OnTouchHit;
            this.tileSelectionComponent.Initialize(this.LevelRunManager.LevelRunModel.TileSelection);
            this.levelRunCameraComponent.Initialize(this.LevelRunManager.LevelRunModel.GameBoard);
        }

        private void InstantiateTile(Tile newTile) {
            this.instantiatorComponent.InstantiateTile(newTile);
        }

        public void Update() {
            this.LevelRunManager.Tick(Time.deltaTime);
        }

        public void Destroy() {
            this.LevelRunManager.LevelRunModel.GameBoard.TileAdded -= InstantiateTile;
            this.inputComponent.TouchStarted -= StartTouch;
            this.inputComponent.TouchEnded -= EndTouch;
            this.inputComponent.TouchHit -= OnTouchHit;
        }

        private void StartTouch() {
            this.touchInProgress = true;
        }

        private void EndTouch() {
            this.touchInProgress = false;
            this.LevelRunManager.FinishSelection();
        }

        private void OnTouchHit(GameObject gameObject) {
            TileComponent tileComponent = gameObject.GetComponent<TileComponent>();
            if (tileComponent == null) {
                return;
            }

            this.LevelRunManager.ProcessTileForSelection(tileComponent.Tile);
        }

    }
}