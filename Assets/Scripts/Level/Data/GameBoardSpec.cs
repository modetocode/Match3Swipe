using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modetocode.Swiper.Level.Data {

    /// <summary>
    /// Represents the specification of a game board.
    /// </summary>
    [Serializable]
    public class GameBoardSpec {
        [SerializeField]
        private int rowCount;
        [SerializeField]
        private int columnCount;
        [SerializeField]
        List<TileType> availableTileTypes;

        public int RowCount { get { return this.rowCount; } }
        public int ColumnCount { get { return this.columnCount; } }
        public IList<TileType> AvailableTileTypes { get { return this.availableTileTypes; } }
    }
}
