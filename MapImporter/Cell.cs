using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapImporter
{
    public class Cell
    {
        public Tile tile { set; get; }
        public bool FlipHorizontal { set; get; }
        public bool FlipVertical { set; get; }
        public int Rotation { set; get; }
    }
}
