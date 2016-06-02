using Modetocode.Swiper.Level.Data;
using System;
using System.Collections.Generic;

namespace Modetocode.Swiper.Util {

    /// <summary>
    /// Responsible for detecting if there are no more moves the player can make and starts popping random tiles.
    /// </summary>
    public class NoMoreMovesDetector : ITickable {

        private float spentTimeInCurrentInterval = 0f;
        private GameBoard gameBoard;

        public NoMoreMovesDetector(GameBoard gameBoard) {
            if (gameBoard == null) {
                throw new ArgumentNullException("gameBoard");
            }

            this.gameBoard = gameBoard;
        }

        public void Tick(float deltaTime) {
            this.spentTimeInCurrentInterval += deltaTime;
            if (this.spentTimeInCurrentInterval >= Constants.LevelRun.NoMoreMovesDetectorExecutionIntervalInSeconds) {
                this.spentTimeInCurrentInterval = 0f;
                this.ExecuteDetector();
            }
        }

        public void OnTickingFinished() {
        }

        private void ExecuteDetector() {
            bool noMoreMoves = this.CheckIfThereAreNoMoreMoves();
            if (noMoreMoves) {
                IList<Tile> randomTileList = new List<Tile>() { this.GetRandomTileFromBoard() };
                GameBoardManager.RemoveTilesFromBoard(this.gameBoard, randomTileList);
                GameBoardManager.FillAndAnimateBoard(this.gameBoard);
            }
        }

        private bool CheckIfThereAreNoMoreMoves() {
            bool[][] isTileVisited = new bool[this.gameBoard.RowCount][];
            for (int i = 0; i < this.gameBoard.RowCount; i++) {
                isTileVisited[i] = new bool[this.gameBoard.ColumnCount];
            }

            for (int i = 0; i < this.gameBoard.RowCount; i++) {
                for (int j = 0; j < this.gameBoard.ColumnCount; j++) {
                    if (this.gameBoard.Tiles[i][j] == null) {
                        continue;
                    }

                    if (isTileVisited[i][j]) {
                        continue;
                    }

                    int tilesInSelection = this.GetTilesInSelection(i, j, isTileVisited);
                    if (tilesInSelection >= 3) {
                        return false;
                    }
                }
            }

            return true;
        }

        private int GetTilesInSelection(int startingRowIndex, int startingColumnIndex, bool[][] isTileVisited) {
            return GetTilesInSelection(startingRowIndex, startingColumnIndex, this.gameBoard.Tiles[startingRowIndex][startingColumnIndex].TileType, isTileVisited);
        }

        private int GetTilesInSelection(int rowIndex, int columnIndex, TileType previousTileType, bool[][] isTileVisited) {
            if (rowIndex < 0 || rowIndex >= this.gameBoard.RowCount) {
                return 0;
            }

            if (columnIndex < 0 || columnIndex >= this.gameBoard.ColumnCount) {
                return 0;
            }

            Tile currentTile = this.gameBoard.Tiles[rowIndex][columnIndex];
            TileType tileType = currentTile.TileType;
            if (tileType != previousTileType) {
                return 0;
            }

            if (isTileVisited[rowIndex][columnIndex]) {
                return 0;

            }

            isTileVisited[rowIndex][columnIndex] = true;

            return 1 + GetTilesInSelection(rowIndex, columnIndex - 1, tileType, isTileVisited) + GetTilesInSelection(rowIndex, columnIndex + 1, tileType, isTileVisited) +
                GetTilesInSelection(rowIndex - 1, columnIndex - 1, tileType, isTileVisited) + GetTilesInSelection(rowIndex - 1, columnIndex, tileType, isTileVisited) + GetTilesInSelection(rowIndex - 1, columnIndex + 1, tileType, isTileVisited) +
                GetTilesInSelection(rowIndex + 1, columnIndex - 1, tileType, isTileVisited) + GetTilesInSelection(rowIndex + 1, columnIndex, tileType, isTileVisited) + GetTilesInSelection(rowIndex + 1, columnIndex + 1, tileType, isTileVisited);
        }

        private Tile GetRandomTileFromBoard() {
            int randomRowIndex = UnityEngine.Random.Range(0, this.gameBoard.RowCount);
            int randomColumnIndex = UnityEngine.Random.Range(0, this.gameBoard.ColumnCount);
            return this.gameBoard.Tiles[randomRowIndex][randomColumnIndex];
        }
    }
}
