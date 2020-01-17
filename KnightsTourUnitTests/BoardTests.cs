using FluentAssertions;
using KnightsTourBlazor.Data.KnightsTour;
using KnightsTourBlazor.Data.KnightsTour.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace KnightsTourUnitTests
{
    [TestFixture]
    public class BoardTests
    {
        private Board _board;
        private IMovementHandler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new MovementHandler();
            _board = new Board(_handler);
        }

        [Test]
        public void ConstructorPopulatesSquaresArrayWith64UniqueSquares()
        {
            //Act

            //Assert
            Assert.AreEqual(64, _board.Squares.Length);
            Assert.IsInstanceOf<Square>(_board.Squares[1, 1]);
            Assert.AreNotSame(_board.Squares[1, 1], _board.Squares[2, 1]);
        }


        [Test]
        public void GetSquareWithValidXAndYReturnsCorrectSquare()
        {
            // Arrange
            var x = 5;
            var y = 5;
            // Act
            var actual = _board.GetSquare(x, y);

            // Assert
            Assert.IsInstanceOf<Square>(actual);
            Assert.AreSame(_board.Squares[x, y], actual);
        }

        [Test]
        public void GetSquareWithInvalidXOrYReturnsNull()
        {
            // Arrange
            var x = 9;
            var y = -1;

            // Act
            var actual = _board.GetSquare(x, y);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public void CountLandedOnSquaresReturnsCorrectNumberOfLandedOnSquares()
        {
            // Arrange
            var squareToLandOn = _board.GetSquare(1, 1);
            squareToLandOn.LandOnSquare();

            // Act
            var actual = _board.CountLandedOnSquares();

            // Assert
            Assert.AreEqual(1, actual);
        }

        [Test]
        public void GetNextSquareToMoveToReturnsBestSquareToMoveTo()
        {
            // Arrange
            var x = 0;
            var y = 0;
            var startingSquare = _board.GetSquare(x, y);
            var expected = _board.GetSquare(1, 2);

            // Act
            var actual = _board.GetNextSquareToMoveTo(startingSquare);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void RemoveLandedOnMovesTakesListOfAllMovesAndReturnsOnesWhereLandedOnIsFalse()
        {
            // Arrange
            var allMoves = new List<Move>
            {
                new Move
                {
                    xMove=1,
                    yMove=2,
                },
                new Move
                {
                    xMove=2,
                    yMove=1
                }
            };

            _board.GetSquare(1, 2).LandOnSquare();

            var expected = new List<Move>
            {
                new Move
                {
                    xMove=2,
                    yMove=1
                }
            };

            // Act
            var actual = _board.RemoveLandedOnMoves(allMoves);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}