﻿using Modetocode.Swiper.Level.Data;
using System;

namespace Modetocode.Swiper.Level {

    /// <summary>
    /// Stores the data of one level
    /// </summary>
    public class LevelRunModel {

        public GameBoard GameBoard { get; private set; }
        public TileSelection TileSelection { get; private set; }

        public LevelRunModel(GameBoard gameBoard) {
            if (gameBoard == null) {
                throw new ArgumentNullException("gameBoard");
            }

            this.GameBoard = gameBoard;
            this.TileSelection = new TileSelection();
        }
    }
}
