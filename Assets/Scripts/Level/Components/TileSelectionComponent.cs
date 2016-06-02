using Modetocode.Swiper.Level.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modetocode.Swiper.Level.Components {

    /// <summary>
    /// Visual representation of a tile selection.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class TileSelectionComponent : MonoBehaviour {

        private LineRenderer lineRenderer;
        private TileSelection tileSelection;

        public void Awake() {
            this.lineRenderer = this.GetComponent<LineRenderer>();
        }

        public void Initialize(TileSelection tileSelection) {
            if (tileSelection == null) {
                throw new ArgumentNullException("tileSelection");
            }
            this.tileSelection = tileSelection;
            this.tileSelection.TileAdded += AddTileConnection;
            this.tileSelection.SelectionFinished += ResetTileConnections;
            this.tileSelection.SelectionShortened += ShortenTileConnection;
        }

        private void AddTileConnection(Tile tile) {
            int numberOfNodes = this.tileSelection.TileSequence.Count;
            this.lineRenderer.SetVertexCount(numberOfNodes);
            this.lineRenderer.SetPosition(numberOfNodes - 1, tile.Position);
        }

        private void ResetTileConnections() {
            this.lineRenderer.SetVertexCount(1);
        }


        private void ShortenTileConnection() {
            IList<Tile> sequence = this.tileSelection.TileSequence;
            this.lineRenderer.SetVertexCount(sequence.Count);
        }

        public void Destroy() {
            this.tileSelection.TileAdded -= AddTileConnection;
            this.tileSelection.SelectionFinished -= ResetTileConnections;
            this.tileSelection.SelectionShortened -= ShortenTileConnection;
        }
    }
}
