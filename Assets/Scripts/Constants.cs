namespace Modetocode.Swiper {

    /// <summary>
    /// Constaints various constants needed through the code.
    /// </summary>
    public static class Constants {
        public static class Animation {
            public static float TileMoveAnimationDurationInSeconds = 0.5f;
        }

        public static class LevelRun {
            public static float TileWorldSpaceSize = 1f;
            public static float HeaderAndFooterScreenSizePercentage = 0.25f;
            public static float NoMoreMovesDetectorExecutionIntervalInSeconds = 1f;
            public static float BaseScorePerTile = 10;
            public static float ScoreIncreasePerNewTile = 5;
            public static float BaseTimeBonusPerTileInSeconds = 0.1f;
            public static float TimeBonusIncreasePerNewTileInSeconds = 0.05f;
            public static float StartingLevelRemainingTimeInSeconds = 60f;
        }

        public static class GameData {
            public static string GameDataFileName = "gameData.gd";
        }
    }
}