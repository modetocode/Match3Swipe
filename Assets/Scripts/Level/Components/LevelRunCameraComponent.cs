using Modetocode.Swiper.Level.Data;
using System;
using UnityEngine;

namespace Modetocode.Swiper.Level.Components {

    /// <summary>
    /// Camera component that is responsible for resizing of camera based on the size of the board
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class LevelRunCameraComponent : MonoBehaviour {

        private Camera levelCamera;

        public void Awake() {
            this.levelCamera = this.GetComponent<Camera>();
            if (!this.levelCamera.orthographic) {
                throw new InvalidOperationException("The camera should be set to orthographic");
            }
        }

        public void Initialize(GameBoard gameBoard) {
            if (gameBoard == null) {
                throw new ArgumentNullException("gameBoard");
            }

            float widthWorldSpace = gameBoard.ColumnCount * Constants.LevelRun.TileWorldSpaceSize;
            float heightWorldSpace = gameBoard.RowCount * Constants.LevelRun.TileWorldSpaceSize;
            float heightCameraSize = heightWorldSpace * 0.5f;
            float widthCameraSize = (widthWorldSpace * 0.5f) / this.levelCamera.aspect;
            this.levelCamera.orthographicSize = Mathf.Max(heightCameraSize, widthCameraSize);
            Vector3 cameraPosition = new Vector3((gameBoard.ColumnCount - 1f) * 0.5f, (gameBoard.RowCount - 1f) * 0.5f, this.levelCamera.transform.position.z);
            this.levelCamera.transform.position = cameraPosition;
        }
    }
}
