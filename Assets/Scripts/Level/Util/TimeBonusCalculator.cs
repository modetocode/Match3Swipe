using Modetocode.Swiper.Level.Data;
using System;
using System.Collections.Generic;

namespace Modetocode.Swiper.Level.Util {

    /// <summary>
    /// Responsible for calculation of bonus time for a given match of tiles
    /// </summary>
    public static class TimeBonusCalculator {

        public static float CalculateBonusTime(IList<Tile> tiles) {
            if (tiles == null) {
                throw new ArgumentNullException("tiles");
            }

            float totalTimeBonus = 0;
            float currentTileTimeBonus = Constants.LevelRun.BaseTimeBonusPerTileInSeconds;
            for (int i = 0; i < tiles.Count; i++) {
                totalTimeBonus += currentTileTimeBonus;
                currentTileTimeBonus += Constants.LevelRun.TimeBonusIncreasePerNewTileInSeconds;
            }

            return totalTimeBonus;
        }
    }
}