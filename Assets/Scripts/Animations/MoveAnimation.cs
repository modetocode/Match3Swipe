using Modetocode.Swiper.Level.Data;
using System;
using UnityEngine;

namespace Modetocode.Swiper.Animations {

    /// <summary>
    /// Represents a move animation of an object
    /// </summary>
    public class MoveAnimation : ObjectAnimation {

        private Vector2 direction;
        private float speed;

        public override void OnTick(float deltaTime) {
            this.ObjectToAnimate.Position += this.speed * this.direction * deltaTime;
        }

        public override void OnTickingFinished() {
        }

        public override void OnAnimationFinished() {
            this.ObjectToAnimate.Position = EndPosition;
        }

        public MoveAnimation(Vector2 startPosition, Vector2 endPosition, float durationInSeconds, IPositionableObject objectToAnimate, Action onMovementFinishedAction)
            : base(startPosition, endPosition, durationInSeconds, objectToAnimate, onMovementFinishedAction) {

            Vector2 distanceVector = endPosition - startPosition;
            this.direction = distanceVector.normalized;
            this.speed = distanceVector.magnitude / durationInSeconds;
            this.ObjectToAnimate.Position = startPosition;
        }
    }
}
