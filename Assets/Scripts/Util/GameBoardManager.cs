using Modetocode.Swiper.Animations;
using Modetocode.Swiper.Level.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modetocode.Swiper.Util {

    /// <summary>
    /// Contains methods that are connected with game board.
    /// </summary>
    public static class GameBoardManager {
        public static GameBoard CreateGameBoard(int rowCount, int columnCount, IList<TileType> availableTileTypes) {
            if (rowCount <= 0) {
                throw new ArgumentOutOfRangeException("rowCount", "Cannot be zero or less.");
            }

            if (columnCount <= 0) {
                throw new ArgumentOutOfRangeException("columnCount", "Cannot be zero or less.");
            }

            if (availableTileTypes == null) {
                throw new ArgumentNullException("availableTileTypes");
            }

            return new GameBoard(rowCount, columnCount, availableTileTypes);
        }

        public static void FillAndAnimateBoard(GameBoard gameBoard) {
            if (gameBoard == null) {
                throw new ArgumentNullException("gameBoard");
            }

            int[] nextRowSpawnYSlot = new int[gameBoard.ColumnCount];
            for (int i = 0; i < gameBoard.ColumnCount; i++) {
                nextRowSpawnYSlot[i] = -1;
            }

            for (int i = gameBoard.RowCount - 1; i >= 0; i--) {
                for (int j = 0; j < gameBoard.ColumnCount; j++) {
                    Slot tileSlot = new Slot(i, j);
                    bool hasTile = gameBoard.HasTileForSlot(tileSlot);
                    if (hasTile) {
                        continue;
                    }

                    Tile newTile = GenerateRandomTile(gameBoard.AvailableTileTypes, tileSlot);
                    Vector2 startTilePosition = GetTilePositionForSlot(new Slot(nextRowSpawnYSlot[i], j), gameBoard);
                    Vector2 endTilePosition = GetTilePositionForSlot(tileSlot, gameBoard);
                    nextRowSpawnYSlot[i]--;
                    Action onMovementFinishedAction = () => {
                        gameBoard.AssignTileToSlot(newTile, tileSlot);
                    };

                    ObjectAnimator.Instance.AddObjectAnimation(
                        startPosition: startTilePosition,
                        endPosition: endTilePosition,
                        durationInSeconds: Constants.Animation.TileMoveAnimationDurationInSeconds,
                        onPositionReachedAction: onMovementFinishedAction,
                        positionableObject: newTile);
                    gameBoard.AddTile(newTile);
                }
            }
        }


        private static Tile GenerateRandomTile(IList<TileType> availableTileTypes, Slot slot) {
            TileType randomTileType = availableTileTypes[UnityEngine.Random.Range(0, availableTileTypes.Count)];
            return new Tile(randomTileType);
        }

        private static Vector2 GetTilePositionForSlot(Slot tileSlot, GameBoard gameBoard) {
            Vector2 tilePosition = new Vector2(tileSlot.ColumnIndex, gameBoard.RowCount - 1 - tileSlot.RowIndex);
            return tilePosition;
        }
    }
}