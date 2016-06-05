using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Modetocode.Swiper.PlayerGameData {
    /// <summary>
    /// Responsible for saving and loading the game data of the players.
    /// </summary>
    public static class GameDataManager {

        public static PlayerGameData LoadGameData() {
            PlayerGameData gameData;
            string gameDataPath = GetGameDataPath();
            if (File.Exists(gameDataPath)) {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Open(gameDataPath, FileMode.Open);
                gameData = (PlayerGameData)binaryFormatter.Deserialize(file);
                file.Close();
            }
            else {
                gameData = new PlayerGameData();
                SaveGameData(gameData);
            }

            return gameData;
        }

        public static void SaveGameData(PlayerGameData gameData) {
            if (gameData == null) {
                throw new ArgumentNullException("gameData");
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Create(GetGameDataPath());
            binaryFormatter.Serialize(file, gameData);
            file.Close();
        }

        private static string GetGameDataPath() {
            return Path.Combine(Application.persistentDataPath, Constants.GameData.GameDataFileName);
        }
    }
}