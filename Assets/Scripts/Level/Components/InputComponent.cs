using System;
using UnityEngine;

namespace Modetocode.Swiper.Level.Components {

    /// <summary>
    /// Responsible for handling input.
    /// </summary>
    public class InputComponent : MonoBehaviour {

        /// <summary>
        /// Event that is thrown when a gameobject with collider is hit during touch. Two consecutive events for the same GameObject are avoided and cannot happen.
        /// </summary>
        public event Action<GameObject> TouchHit;
        public event Action TouchStarted;
        public event Action TouchEnded;

        private int touchFingerId = -1;
        private GameObject lastObjectTouchHit = null;

        public void Update() {
            this.HandleTouchInput();
        }

        private void HandleTouchInput() {
            for (int i = 0; i < Input.touches.Length; i++) {
                Touch currentTouch = Input.touches[i];

                // Only one finger will be tracked; If the player has already put a finger on the screen the other fingers will be unregistered
                if (currentTouch.phase == TouchPhase.Began && touchFingerId == -1) {
                    touchFingerId = currentTouch.fingerId;
                    if (this.TouchStarted != null) {
                        this.TouchStarted();
                    }
                }

                if (touchFingerId != currentTouch.fingerId) {
                    continue;
                }

                if (currentTouch.phase == TouchPhase.Stationary) {
                    continue;
                }

                if (currentTouch.phase == TouchPhase.Canceled) {
                    this.touchFingerId = -1;
                    this.lastObjectTouchHit = null;
                    continue;
                }

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100)) {
                    GameObject hitObject = hit.collider.gameObject;
                    if (!hitObject.Equals(this.lastObjectTouchHit)) {
                        if (this.TouchHit != null) {
                            this.TouchHit(hitObject);
                        }

                        this.lastObjectTouchHit = hitObject;
                    }

                }

                if (currentTouch.phase == TouchPhase.Ended) {
                    this.touchFingerId = -1;
                    this.lastObjectTouchHit = null;
                    if (this.TouchEnded != null) {
                        this.TouchEnded();
                    }

                }
            }
        }
    }
}

