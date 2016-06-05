using System;
using UnityEngine;

namespace Modetocode.Swiper.PlayerGameData {

    /// <summary>
    /// Constains data for a player that will be saved on game exit.
    /// </summary>
    [Serializable]
    public class PlayerGameData {

        [SerializeField]
        private float highscore = 0f;

        public float Highscore {
            get { return this.highscore; }
            set {
                this.highscore = value;
            }
        }
    }
}
