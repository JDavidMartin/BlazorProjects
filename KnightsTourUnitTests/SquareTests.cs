using KnightsTourBlazor.Data.KnightsTour;
using NUnit.Framework;

namespace KnightsTourUnitTests
{
    [TestFixture]
    public class SquareTests
    {
        private Square _square;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SquareShouldBeInitiatedWithLandedOnFalse()
        {
            // Arrange

            _square = new Square(1, 1);
            // Act

            // Assert
            Assert.IsInstanceOf<Square>(_square);
            Assert.IsFalse(_square.ReturnLandedOnStatus());
        }


        [Test]
        public void LandingOnASquareShouldChangeLandedOnStatusToTrue()
        {
            // Arrange
            _square = new Square(1, 1);

            // Act
            _square.LandOnSquare();

            // Assert
            Assert.IsTrue(_square.ReturnLandedOnStatus());
        }
    }
}
