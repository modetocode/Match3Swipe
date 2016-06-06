namespace Modetocode.Swiper {

    /// <summary>
    /// Constaints various constants needed through the code.
    /// </summary>
    public static class Constants {
        public static class Animation {
            public const float TileMoveAnimationDurationInSeconds = 0.5f;
        }

        public static class LevelRun {
            public const float TileWorldSpaceSize = 1f;
            public const float HeaderAndFooterScreenSizePercentage = 0.25f;
            public const float NoMoreMovesDetectorExecutionIntervalInSeconds = 1f;
        }

        public static class GameData {
            public const string GameDataFileName = "gameData.gd";
        }

        public static class GameSettings {
            public const string GameConstantsAssetName = "GameConstants";
            public const string AssetsExtension = ".asset";
            public const string ResourcesPath = "Assets/Resources/";
            public const string GameSettingsRelativePath = "GameSettings/";
            public const string GameSettingsMenuName = "Game Settings";
            public const string GameConstantsMenuName = "Game Constants";
        }
    }
}