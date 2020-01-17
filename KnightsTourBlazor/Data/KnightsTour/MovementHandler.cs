using KnightsTourBlazor.Data.KnightsTour;
using KnightsTourBlazor.Data.KnightsTour.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KnightsTourBlazor.Data.KnightsTour
{
    public class MovementHandler : IMovementHandler
    {
        private int[] xMoveArray = new int[] { -2, -2, -1, -1, 1, 1, 2, 2 };
        private int[] yMoveArray = new int[] { -1, 1, -2, 2, -2, 2, -1, 1 };

        public MovementHandler()
        {
        }

        public List<Move> ReturnAllPossibleMoves(int startingX, int startingY)
        {
            var allMoves = new List<Move>();
            for (var i = 0; i < 8; i++)
            {
                if (NumberWithinBoard(startingX + xMoveArray[i]) && NumberWithinBoard(startingY + yMoveArray[i]))
                {
                    allMoves.Add(new Move
                    {
                        xMove = startingX + xMoveArray[i],
                        yMove = startingY + yMoveArray[i],
                    });
                }
            }
            return allMoves;
        }

        public List<Move> RemoveInvalidMoves(List<Move> allMoves)
        {
            return allMoves.Where(x => x.allowedMove == true).ToList();
        }

        public List<Move> ReturnNextPossibleMoves(int startingX, int startingY)
        {
            var allPossibleMoves = ReturnAllPossibleMoves(startingX, startingY);
            //RemoveLandedOnMoves(allPossibleMoves);

            return RemoveInvalidMoves(allPossibleMoves);
        }

        //public void RemoveLandedOnMoves(List<Move> allMoves)
        //{
        //    foreach (var move in allMoves)
        //    {
        //        if (_board.GetSquare(move.xMove, move.yMove).returnLandedOnStatus())
        //        {
        //            move.allowedMove = false;
        //        }
        //    }
        //}
        public bool NumberWithinBoard(int position)
        {
            return position >= 0 && position < 8 ? true : false;
        }

        public void CountOnwardsMoves(List<Move> possibleMoves)
        {
            foreach (var Move in possibleMoves)
            {
                Move.numOnwardMoves = ReturnNextPossibleMoves(Move.xMove, Move.yMove).Count;
            }
        }
    }
}
