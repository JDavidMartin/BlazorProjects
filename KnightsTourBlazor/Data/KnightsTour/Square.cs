using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightsTourBlazor.Data.KnightsTour
{
    public class Square
    {
        private bool landedOn { get; set; } = false;
        public int x { get; set; }
        public int y { get; set; }

        public Square(int xPosition, int yPosition)
        {
            x = xPosition;
            y = yPosition;
        }

        public void LandOnSquare()
        {
            landedOn = true;
        }

        public bool ReturnLandedOnStatus()
        {
            return landedOn;
        }
        public void ResetSquare()
        {
            landedOn = false;
        }
    }
}
