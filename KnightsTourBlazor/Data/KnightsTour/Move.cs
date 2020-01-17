using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightsTourBlazor.Data.KnightsTour
{
    public class Move
    {
        public int xMove { get; set; }
        public int yMove { get; set; }
        public bool allowedMove { get; set; } = true;
        public int numOnwardMoves { get; set; }
    }
}
