using Modetocode.Swiper.GameSettings;
using Modetocode.Swiper.Level.Data;
using System;
using System.Collections.Generic;

namespace Modetocode.Swiper.Level.Util {

    /// <summary>
    /// Responsible for calculation of scores of a level.
    /// </summary>
    public static class ScoreCalculator {

        public static float CalculateScore(IList<Tile> tiles) {
            if (tiles == null) {
                throw new ArgumentNullException("tiles");
            }

            float totalTileScore = 0;
            GameConstants gameConstants = GameSettingsManager.GetGameConstants();
            float currentTileScore = gameConstants.BaseScorePerTile;
            for (int i = 0; i < tiles.Count; i++) {
                totalTileScore += currentTileScore;
                currentTileScore += gameConstants.ScoreIncreasePerNewTile;
            }

            return totalTileScore;
        }
    }
}
