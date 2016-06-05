using Modetocode.Swiper.PlayerGameData;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Modetocode.Swiper.Level.Components {
    /// <summary>
    /// Responsible for showing and updating GUI related components.
    /// </summary>
    public class LevelRunGuiComponent : MonoBehaviour {
        [SerializeField]
        private LevelRunComponent levelRunComponent;
        [SerializeField]
        private Text remainingTimeLabelText;
        [SerializeField]
        private Text scoreLabelText;
        [SerializeField]
        private Button startButton;
        [SerializeField]
        private Text highscoreLabelText;

        private LevelRunModel levelRunModel;

        public void Awake() {
            if (this.levelRunComponent == null) {
                throw new NullReferenceException("remainingTimeLabelText reference is null");
            }

            if (this.remainingTimeLabelText == null) {
                throw new NullReferenceException("remainingTimeLabelText reference is null");
            }

            if (this.scoreLabelText == null) {
                throw new NullReferenceException("scoreLabelText reference is null");
            }

            if (this.startButton == null) {
                throw new NullReferenceException("startButton reference is null");
            }

            if (this.highscoreLabelText == null) {
                throw new NullReferenceException("highscoreLabelText reference is null");
            }

            this.startButton.onClick.AddListener(StartGame);
            this.levelRunComponent.LevelFinished += ShowStartButton;
            this.levelRunComponent.LevelFinished += UpdateHighscore;
        }

        private void ShowStartButton() {
            this.startButton.gameObject.SetActive(true);
        }

        private void StartGame() {
            this.startButton.gameObject.SetActive(false);
            this.levelRunComponent.StartGame();
        }

        public void Start() {
            if (!this.levelRunComponent.IsInitialized) {
                this.levelRunComponent.Initialized += InitializeGUI;
                return;
            }

            this.InitializeGUI();
        }

        private void InitializeGUI() {
            this.levelRunComponent.Initialized -= InitializeGUI;
            this.levelRunModel = this.levelRunComponent.LevelRunModel;
            this.UpdateHighscore();
        }

        public void Update() {
            if (this.levelRunModel == null) {
                return;
            }

            if (!this.levelRunComponent.GameInProgress) {
                return;
            }

            this.remainingTimeLabelText.text = string.Format("{0:f1}", this.levelRunModel.RemainingTimeInSeconds);
            this.scoreLabelText.text = this.levelRunModel.Score.ToString();
        }

        public void Destroy() {
            this.levelRunComponent.LevelFinished -= ShowStartButton;
            this.levelRunComponent.LevelFinished -= UpdateHighscore;
        }

        private void UpdateHighscore() {
            PlayerGameData.PlayerGameData gameData = GameDataManager.LoadGameData();
            this.highscoreLabelText.text = gameData.Highscore.ToString();
        }
    }
}