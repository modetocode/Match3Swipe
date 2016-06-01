namespace Modetocode.Swiper.Level.Data {

    /// <summary>
    /// Represents one slot on the board with a given row and column index
    /// </summary>
    public class Slot {

        public int RowIndex { get; private set; }
        public int ColumnIndex { get; private set; }

        public Slot(int rowIndex, int columnIndex) {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
        }

    }
}
