using Modetocode.Swiper.Level.Data;
using UnityEngine;

namespace Modetocode.Swiper.GameSettings {
    /// <summary>
    /// Contains various game constants that affect the gameplay of the game.
    /// </summary>
    public class GameConstants : ScriptableObject {
        [SerializeField]
        private GameBoardSpec gameBoardSpec;
        [SerializeField]
        private float baseScorePerTile = 10;
        [SerializeField]
        private float scoreIncreasePerNewTile = 5;
        [SerializeField]
        private float baseTimeBonusPerTileInSeconds = 0.1f;
        [SerializeField]
        private float timeBonusIncreasePerNewTileInSeconds = 0.05f;
        [SerializeField]
        private float startingLevelRemainingTimeInSeconds = 60f;

        public GameBoardSpec GameBoardSpec { get { return this.gameBoardSpec; } }
        public float BaseScorePerTile { get { return this.baseScorePerTile; } }
        public float ScoreIncreasePerNewTile { get { return this.scoreIncreasePerNewTile; } }
        public float BaseTimeBonusPerTileInSeconds { get { return this.baseTimeBonusPerTileInSeconds; } }
        public float TimeBonusIncreasePerNewTileInSeconds { get { return this.timeBonusIncreasePerNewTileInSeconds; } }
        public float StartingLevelRemainingTimeInSeconds { get { return this.startingLevelRemainingTimeInSeconds; } }
    }
}