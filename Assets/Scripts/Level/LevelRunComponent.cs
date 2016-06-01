using UnityEngine;

namespace Modetocode.Swiper.Level {

    /// <summary>
    /// Responsible for showing the logic on the screen.
    /// </summary>
    public class LevelRunComponent : MonoBehaviour {

        private LevelRunManager LevelRunManager { get; set; }

        public void Start() {
            this.LevelRunManager = new LevelRunManager();
        }

        public void Update() {
            this.LevelRunManager.Tick(Time.deltaTime);
        }
    }
}