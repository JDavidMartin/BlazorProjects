using KnightsTourBlazor.Data.KnightsTour;
using KnightsTourBlazor.Data.KnightsTour.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnightsTourUnitTests
{

    [TestFixture]
    public class KnightsTourGameTests
    {
        private KnightsTourGame _game;
        private IBoard _board;
        private IMovementHandler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new MovementHandler();
            _board = new Board(_handler);
            _game = new KnightsTourGame(_board);
        }

        [Test]
        public void GameReturnsA2dStringArray()
        {
            // Arrange

            // Act
            var actual = _game.TourTheBoard(5, 4);

            // Assert
            Assert.IsInstanceOf<string[,]>(actual);
        }
    }

}
