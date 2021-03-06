﻿namespace Modetocode.Swiper.Util {

    /// <summary>
    /// Represents an abstraction that will can be ticked.
    /// </summary>
    public interface ITickable {
        /// <summary>
        /// Executed regularly when the ticking is not paused.
        /// </summary>
        /// <param name="deltaTime"></param>
        void Tick(float deltaTime);

        /// <summary>
        /// Executed once when the ticking is finished.
        /// </summary>
        void OnTickingFinished();
    }
}