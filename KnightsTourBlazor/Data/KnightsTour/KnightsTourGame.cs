using KnightsTourBlazor.Data.KnightsTour.Interfaces;

namespace KnightsTourBlazor.Data.KnightsTour
{
    public class KnightsTourGame
    {
        private IBoard _board;
        public KnightsTourGame(IBoard board)
        {
            _board = board;
        }
        public TourData TourTheBoard(int x, int y)
        {
            string[,] movesArray = new string[8, 8];
            _board.ResetBoard();
            int iteration = 0;

            var chosenSquare = _board.GetSquare(x, y);
            chosenSquare.LandOnSquare();
            movesArray[x, y] = iteration.ToString() ;

            while (_board.CountLandedOnSquares() <= 63)
            {
                iteration++;
                try
                {
                    chosenSquare = _board.GetNextSquareToMoveTo(chosenSquare);
                }
                catch
                {
                    break;
                }

                chosenSquare.LandOnSquare();
                movesArray[chosenSquare.x, chosenSquare.y] = iteration.ToString();
            }

            var tourData = new TourData
            {
                MovesArray = movesArray,
                NumberMoves = iteration
            };

            return tourData;
        }
    }
}
