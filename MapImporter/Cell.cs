namespace MapImporter
{
    public class Cell
    {
        public Tile Tile { set; get; }
        public bool FlipHorizontal { set; get; }
        public bool FlipVertical { set; get; }
        public int Rotation { set; get; }

        public Cell(Tile tile)
        {
            Tile = tile;
        }
    }
}
