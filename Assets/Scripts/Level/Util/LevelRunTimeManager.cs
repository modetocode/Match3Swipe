using Modetocode.Swiper.Util;
using System;

namespace Modetocode.Swiper.Level.Util {
    /// <summary>
    /// Responsible for time management of one level run.
    /// </summary>
    public class LevelRunTimeManager : ITickable {

        public event Action TimePassed;

        public float RemainingTimeInSeconds { get; private set; }
        private bool HasTimePassed { get; set; }

        public LevelRunTimeManager() {
            this.Initialize();
        }

        public void Tick(float deltaTime) {
            if (this.HasTimePassed) {
                throw new InvalidOperationException("The ticking has already been finished.");
            }

            this.RemainingTimeInSeconds -= deltaTime;
            if (this.RemainingTimeInSeconds <= 0f) {
                this.RemainingTimeInSeconds = 0f;
                this.HasTimePassed = true;
                if (this.TimePassed != null) {
                    this.TimePassed();
                }
            }
        }

        public void OnTickingFinished() {
        }


        public void AddTime(float timeInSeconds) {
            if (timeInSeconds < 0) {
                throw new ArgumentOutOfRangeException("timeInSeconds", "Cannot be less than zero.");
            }

            if (this.HasTimePassed) {
                throw new InvalidOperationException("You cannot add time when ticking has already been finished.");
            }

            this.RemainingTimeInSeconds += timeInSeconds;
        }

        public void Reset() {
            this.Initialize();
        }

        private void Initialize() {
            this.RemainingTimeInSeconds = Constants.LevelRun.StartingLevelRemainingTimeInSeconds;
            this.HasTimePassed = false;
        }
    }
}
