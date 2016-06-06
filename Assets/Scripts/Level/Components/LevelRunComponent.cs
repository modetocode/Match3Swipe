using Modetocode.Swiper.GameSettings;
using Modetocode.Swiper.Level.Data;
using Modetocode.Swiper.PlayerGameData;
using System;
using UnityEngine;

namespace Modetocode.Swiper.Level.Components {

    /// <summary>
    /// Responsible for showing the logic on the screen.
    /// </summary>
    public class LevelRunComponent : MonoBehaviour {

        public event Action Initialized;
        public event Action LevelFinished;

        [SerializeField]
        private InstantiatorComponent instantiatorComponent;
        [SerializeField]
        private InputComponent inputComponent;
        [SerializeField]
        private TileSelectionComponent tileSelectionComponent;
        [SerializeField]
        private LevelRunCameraComponent levelRunCameraComponent;

        public bool IsInitialized { get; private set; }
        public LevelRunModel LevelRunModel { get { return this.LevelRunManager.LevelRunModel; } }
        public bool GameInProgress { get; private set; }
        private LevelRunManager LevelRunManager { get; set; }
        private bool TouchInProgress { get; set; }

        public void Awake() {
            if (this.instantiatorComponent == null) {
                throw new NullReferenceException("instantiatorComponent reference is null");
            }

            if (this.inputComponent == null) {
                throw new NullReferenceException("inputComponent reference is null");
            }

            if (this.tileSelectionComponent == null) {
                throw new NullReferenceException("tileSelectionComponent reference is null");
            }

            if (this.levelRunCameraComponent == null) {
                throw new NullReferenceException("levelRunCameraComponent reference is null");
            }
        }

        public void StartGame() {
            this.LevelRunManager.Start();
            this.GameInProgress = true;
            this.SubscribeForInputEvents();
        }

        public void Start() {
            GameBoardSpec gameBoardSpec = GameSettingsManager.GetGameConstants().GameBoardSpec;
            this.LevelRunManager = new LevelRunManager(gameBoardSpec);
            this.LevelRunManager.LevelFinished += OnLevelFinishedHandler;
            this.LevelRunModel.GameBoard.TileAdded += InstantiateTile;
            this.TouchInProgress = false;
            this.GameInProgress = false;
            this.tileSelectionComponent.Initialize(this.LevelRunModel.TileSelection);
            this.levelRunCameraComponent.Initialize(this.LevelRunModel.GameBoard);
            this.IsInitialized = true;
            if (this.Initialized != null) {
                this.Initialized();
            }
        }

        private void InstantiateTile(Tile newTile) {
            this.instantiatorComponent.InstantiateTile(newTile);
        }

        public void Update() {
            if (!this.GameInProgress) {
                return;
            }

            this.LevelRunManager.Tick(Time.deltaTime);
        }

        public void Destroy() {
            this.LevelRunManager.LevelFinished -= OnLevelFinishedHandler;
            this.LevelRunModel.GameBoard.TileAdded -= InstantiateTile;
            this.UnsubsribeFromInputEvents();
        }

        private void StartTouch() {
            this.TouchInProgress = true;
        }

        private void EndTouch() {
            this.TouchInProgress = false;
            this.LevelRunManager.FinishSelection();
        }

        private void OnTouchHit(GameObject gameObject) {
            if (!this.TouchInProgress) {
                return;
            }

            TileComponent tileComponent = gameObject.GetComponent<TileComponent>();
            if (tileComponent == null) {
                return;
            }

            this.LevelRunManager.ProcessTileForSelection(tileComponent.Tile);
        }

        private void OnLevelFinishedHandler() {
            this.UpdateHighscore();
            this.GameInProgress = false;
            this.TouchInProgress = false;
            this.UnsubsribeFromInputEvents();
            this.LevelRunManager.ResetData();
            if (this.LevelFinished != null) {
                this.LevelFinished();
            }
        }

        private void UpdateHighscore() {
            PlayerGameData.PlayerGameData gameData = GameDataManager.LoadGameData();
            float newScore = this.LevelRunModel.Score;
            if (newScore > gameData.Highscore) {
                gameData.Highscore = this.LevelRunModel.Score;
                GameDataManager.SaveGameData(gameData);
            }
        }

        private void SubscribeForInputEvents() {
            this.inputComponent.TouchStarted += StartTouch;
            this.inputComponent.TouchEnded += EndTouch;
            this.inputComponent.TouchHit += OnTouchHit;
        }

        private void UnsubsribeFromInputEvents() {
            this.inputComponent.TouchStarted -= StartTouch;
            this.inputComponent.TouchEnded -= EndTouch;
            this.inputComponent.TouchHit -= OnTouchHit;
        }
    }
}