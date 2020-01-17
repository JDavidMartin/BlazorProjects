using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightsTourBlazor.Data.KnightsTour.Interfaces
{
    public interface IMovementHandler
    {
        public List<Move> ReturnAllPossibleMoves(int startingX, int startingY);
        public List<Move> RemoveInvalidMoves(List<Move> allMoves);
        //public void RemoveLandedOnMoves(List<Move> allMoves);
        public List<Move> ReturnNextPossibleMoves(int startingX, int startingY);
        public void CountOnwardsMoves(List<Move> possibleMoves);

    }
}
