using Modetocode.Swiper.Level.Data;
using Modetocode.Swiper.Util;
using System;
using UnityEngine;

namespace Modetocode.Swiper.Animations {

    /// <summary>
    /// Represents one animation that can be added to one positionable object
    /// </summary>
    public abstract class ObjectAnimation : ITickable {

        public event Action<ObjectAnimation> AnimationFinised;

        public Vector2 StartPositon { get; private set; }
        public Vector2 EndPosition { get; private set; }
        public float DurationInSeconds { get; private set; }
        public IPositionableObject ObjectToAnimate { get; private set; }
        public Action OnAnimationFinishedAction { get; private set; }

        private float spentTime = 0;
        private bool hasFinished = false;

        public void Tick(float deltaTime) {
            if (this.hasFinished) {
                throw new InvalidOperationException("The animation has finished");
            }

            this.OnTick(deltaTime);
            spentTime += deltaTime;
            if (spentTime >= this.DurationInSeconds) {
                this.hasFinished = true;
                this.OnAnimationFinishedAction();
                this.OnAnimationFinished();
                if (this.AnimationFinised != null) {
                    this.AnimationFinised(this);
                }
            }

        }

        public abstract void OnTick(float deltaTime);
        public abstract void OnTickingFinished();
        public abstract void OnAnimationFinished();

        protected ObjectAnimation(Vector2 startPosition, Vector2 endPosition, float durationInSeconds, IPositionableObject objectToAnimate, Action onAnimationFinishedAction) {
            if (durationInSeconds <= 0) {
                throw new ArgumentOutOfRangeException("durationInSeconds", "Cannot be zero or less");
            }

            if (objectToAnimate == null) {
                throw new ArgumentNullException("objectToAnimate");
            }

            if (onAnimationFinishedAction == null) {
                throw new ArgumentNullException("onAnimationFinishedAction");
            }

            this.StartPositon = startPosition;
            this.EndPosition = endPosition;
            this.DurationInSeconds = durationInSeconds;
            this.ObjectToAnimate = objectToAnimate;
            this.OnAnimationFinishedAction = onAnimationFinishedAction;
        }
    }
}
