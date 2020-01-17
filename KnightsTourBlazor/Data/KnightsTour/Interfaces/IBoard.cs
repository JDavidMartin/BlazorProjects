using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightsTourBlazor.Data.KnightsTour.Interfaces
{
    public interface IBoard
    {
        public Square GetSquare(int x, int y);
        public int CountLandedOnSquares();
        public Square GetNextSquareToMoveTo(Square currentSquare);
        public List<Move> RemoveLandedOnMoves(List<Move> allMoves);
        public void ResetBoard();
    }
}
