using FluentAssertions;
using KnightsTourBlazor.Data.KnightsTour;
using KnightsTourBlazor.Data.KnightsTour.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnightsTourUnitTests
{

    [TestFixture]
    public class MovementHandlerTests
    {
        private IMovementHandler _movementHandler;
        private IBoard _board;

        [SetUp]
        public void Setup()
        {
            _board = new Board(_movementHandler);
            _movementHandler = new MovementHandler();
        }

        [Test]
        public void GetPossibleMovesForAValidSquareCorrectlyReturnsExpectedArrayOfPossibleMoves()
        {
            // Arrange
            var startingX = 4;
            var startingY = 4;

            var expected = GetDefaultMoveSet();

            // Act
            var actual = _movementHandler.ReturnAllPossibleMoves(startingX, startingY);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);
            actual.Should().BeEquivalentTo(expected);
        }

        //[Test]
        //public void RemoveLandedOnMovesSetsAllowedMoveToFalseForMovesWhichHaveAlreadyBeenLandedOn()
        //{
        //    // Arrange
        //    _board.GetSquare(2, 3).LandOnSquare();
        //    _board.GetSquare(2, 5).LandOnSquare();
        //    var initialMoves = GetDefaultMoveSet();

        //    var expected = GetDefaultMoveSet();
        //    expected[0].allowedMove = false;
        //    expected[1].allowedMove = false;

        //    // Act
        //    _movementHandler.RemoveLandedOnMoves(initialMoves);

        //    // Assert
        //    initialMoves.Should().BeEquivalentTo(expected);
        //}


        [Test]
        public void ReturnNextPossibleMovesReturnsNextPossibleValidMovesNormalMove()
        {
            // Arrange
            var startingX = 4;
            var startingY = 4;
            var expected = GetDefaultMoveSet();
            // Act
            var actual = _movementHandler.ReturnNextPossibleMoves(startingX, startingY);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ReturnNextPossibleMovesReturnsNextPossibleValidMovesOnTheEdgeMove()
        {
            // Arrange
            var x = 0;
            var y = 0;
            var expected = new List<Move>
            {
                new Move
                {
                    xMove=2,
                    yMove=1,
                    allowedMove=true
                },
                new Move
                {
                    xMove=1,
                    yMove=2,
                    allowedMove=true
                }
            };

            // Act
            var actual = _movementHandler.ReturnNextPossibleMoves(x, y);

            // Assert
            actual.Should().BeEquivalentTo(expected);

        }

        [Test]
        public void ReturnNextPossibleMovesReturnsNextPossibleValidMoveOnlyOneMoveLeft()
        {
            // Arrange
            var x = 4;
            var y = 4;
            _board.GetSquare(2, 3).LandOnSquare();
            _board.GetSquare(3, 2).LandOnSquare();
            _board.GetSquare(5, 2).LandOnSquare();
            _board.GetSquare(2, 5).LandOnSquare();
            _board.GetSquare(5, 6).LandOnSquare();
            _board.GetSquare(6, 5).LandOnSquare();
            _board.GetSquare(6, 3).LandOnSquare();

            var expected = new List<Move>
            {
                new Move
                {
                    xMove=3,
                    yMove=6,
                    allowedMove=true
                }
            };

            // Act
            var actual = _movementHandler.ReturnNextPossibleMoves(x, y);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ReturnNextPossibleMovesReturnsNextPossibleValidMoveOnlyNoMovesLeft()
        {
            var x = 4;
            var y = 4;
            _board.GetSquare(2, 3).LandOnSquare();
            _board.GetSquare(3, 2).LandOnSquare();
            _board.GetSquare(5, 2).LandOnSquare();
            _board.GetSquare(2, 5).LandOnSquare();
            _board.GetSquare(5, 6).LandOnSquare();
            _board.GetSquare(6, 5).LandOnSquare();
            _board.GetSquare(6, 3).LandOnSquare();
            _board.GetSquare(3, 6).LandOnSquare();

            // Act
            var actual = _movementHandler.ReturnNextPossibleMoves(x, y);

            // Assert
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        public void CountOnwardsMovesUpdatesExistingListOfMoveWithExpectedValue()
        {
            // Arrange
            var initialMoves = new List<Move>
            {
                new Move
                {
                    xMove=0,
                    yMove=0,
                    allowedMove=true,
                    numOnwardMoves=0
                }
            };
            var expected = initialMoves;
            expected[0].numOnwardMoves = 2;

            // Act
            _movementHandler.CountOnwardsMoves(initialMoves);

            // Assert
            initialMoves.Should().BeEquivalentTo(expected);
        }


        private List<Move> GetDefaultMoveSet()
        {
            return new List<Move>
            {
                new Move
                {
                    xMove = 2,
                    yMove = 3,
                    allowedMove=true
                },
                new Move
                {
                    xMove = 2,
                    yMove = 5,
                    allowedMove=true
                },
                new Move
                {
                    xMove = 3,
                    yMove = 2,
                    allowedMove=true
                },
                new Move
                {
                    xMove = 3,
                    yMove = 6,
                    allowedMove=true
                },
                new Move
                {
                    xMove = 5,
                    yMove = 2,
                    allowedMove=true
                },
                new Move
                {
                    xMove = 5,
                    yMove = 6,
                    allowedMove=true
                },
                new Move
                {
                    xMove = 6,
                    yMove = 3,
                    allowedMove=true
                },
                new Move
                {
                    xMove = 6,
                    yMove = 5,
                    allowedMove=true
                },
            };
        }
    }
}


