using KnightsTourBlazor.Data.KnightsTour.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnightsTourBlazor.Data.KnightsTour
{
    public class Board : IBoard
    {
        private int maxX = 8;
        private int maxY = 8;
        public Square[,] Squares = new Square[8, 8];
        private IMovementHandler _movementHandler;
        public Board(IMovementHandler movementHandler)
        {
            _movementHandler = movementHandler;

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    Squares[x, y] = new Square(x, y);
                }
            }
        }

        public Square GetSquare(int x, int y)
        {
            if (x >= 0 && x < maxX && y >= 0 && y < maxY)
            {
                return Squares[x, y];
            }

            return null;
        }

        public int CountLandedOnSquares()
        {
            var totalLandedOn = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (Squares[x, y].ReturnLandedOnStatus())
                    {
                        totalLandedOn++;
                    }
                }
            }

            return totalLandedOn;
        }

        public Square GetNextSquareToMoveTo(Square currentSquare)
        {
            var allMoves = _movementHandler.ReturnNextPossibleMoves(currentSquare.x, currentSquare.y);
            var allAllowedMoves = RemoveLandedOnMoves(allMoves);
            _movementHandler.CountOnwardsMoves(allAllowedMoves);

            var LowestNumberMoves = allAllowedMoves.Where(x => x.numOnwardMoves != 0).Min(x => x.numOnwardMoves);
            var potentialMoves = allAllowedMoves.Where(x => x.numOnwardMoves == LowestNumberMoves).ToList();

            //If multiple, tiebreak
            var optimalMove = potentialMoves[0];
            if (potentialMoves.Count != 1)
            {
                optimalMove = TieBreak(potentialMoves);
            }

            var optimalSquare = GetSquare(optimalMove.xMove, optimalMove.yMove);

            return optimalSquare;
        }

        public List<Move> RemoveLandedOnMoves(List<Move> allMoves)
        {
            return allMoves.Where(x => GetSquare(x.xMove, x.yMove).ReturnLandedOnStatus() == false).ToList();
        }
        public Move TieBreak(List<Move> allMoves)
        {
            var tieBreakResults = new List<int>();
            //For each potential move, find one with fewest onward moves again
            foreach (var move in allMoves)
            {
                var onwardMoves = _movementHandler.ReturnNextPossibleMoves(move.xMove, move.yMove);
                var onwardAllowedMoves = RemoveLandedOnMoves(onwardMoves);
                _movementHandler.CountOnwardsMoves(onwardAllowedMoves);
                if (onwardAllowedMoves.Count != 0)
                {
                    var LowestOnwardNumberMoves = onwardAllowedMoves.Where(x => x.numOnwardMoves != 0).Min(x => x.numOnwardMoves);
                    tieBreakResults.Add(LowestOnwardNumberMoves);
                }
            }

            //See how many 
            var lowestNumberOnwardMoves = tieBreakResults.Min(x => x);
            var potentialResults = tieBreakResults.Where(x => x == lowestNumberOnwardMoves).ToList();

            if (potentialResults.Count != 1)
            {
                var distances = new List<double>();
                foreach (var move in allMoves)
                {
                    distances.Add(getModulus(move.xMove, move.yMove));
                }
                var indexOfLowest = Array.IndexOf(distances.ToArray(), distances.Max(x => x));
                return allMoves[indexOfLowest];
            }
            else
            {
                var indexOfLowest = Array.IndexOf(tieBreakResults.ToArray(), lowestNumberOnwardMoves);
                return allMoves[indexOfLowest];
            }
        }

        private double getModulus(int x, int y)
        {
            return Math.Sqrt(Math.Pow((7 / 2 - x), 2) + Math.Pow((7 / 2 - y), 2));
        }

        public void ResetBoard()
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    GetSquare(i, j).ResetSquare();
                }
            }
        }
    }
}
