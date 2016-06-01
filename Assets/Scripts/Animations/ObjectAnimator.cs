using Modetocode.Swiper.Level.Data;
using Modetocode.Swiper.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modetocode.Swiper.Animations {

    /// <summary>
    /// Responsible for animation of an positionable objects
    /// </summary>
    public class ObjectAnimator : Singleton<ObjectAnimator>, ITickable {

        private IList<ObjectAnimation> activeAnimations;

        public void AddObjectAnimation(Vector2 startPosition, Vector2 endPosition, float durationInSeconds, Action onPositionReachedAction, IPositionableObject positionableObject) {
            if (durationInSeconds <= 0) {
                throw new ArgumentOutOfRangeException("durationInSeconds", "Cannot be zero or less.");
            }
            if (onPositionReachedAction == null) {
                throw new ArgumentNullException("onPositionReachedAction");
            }

            if (positionableObject == null) {
                throw new ArgumentNullException("positionableObject");
            }

            if (this.activeAnimations == null) {
                this.activeAnimations = new List<ObjectAnimation>();
            }

            ObjectAnimation newAnimation = new MoveAnimation(startPosition, endPosition, durationInSeconds, positionableObject, onPositionReachedAction);
            newAnimation.AnimationFinised += RemoveAnimation;
            this.activeAnimations.Add(newAnimation);
        }

        private void RemoveAnimation(ObjectAnimation animation) {
            animation.AnimationFinised -= RemoveAnimation;
            this.activeAnimations.Remove(animation);
        }

        public void Tick(float deltaTime) {
            if (this.activeAnimations == null) {
                return;
            }

            for (int i = 0; i < this.activeAnimations.Count; i++) {
                this.activeAnimations[i].Tick(deltaTime);
            }
        }

        public void OnTickingFinished() {
            for (int i = 0; i < this.activeAnimations.Count; i++) {
                this.activeAnimations[i].AnimationFinised -= RemoveAnimation;
            }

            this.activeAnimations = null;
        }
    }
}
