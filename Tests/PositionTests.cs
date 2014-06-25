namespace MarsRovers.Tests
{
    using System;

    using NFluent;

    using NUnit.Framework;

    public class PositionTests
    {
        [Test]
        public void IsEqualWorksByValue()
        {
            var firstPosition = new Position(2, 3, "N");
            var secondPosition = new Position(2, 3, "N");
            var nonEqualPosition = new Position(1, 3, "N");

            Check.That(secondPosition).IsEqualTo(firstPosition);
            Check.That(nonEqualPosition).IsNotEqualTo(firstPosition);
        }

        [Test]
        public void TurnLeftWorks()
        {
            var position = Position.Parse("0,0,N");
            var newPosition = position.TurnLeft();
            Check.That(newPosition).IsEqualTo(Position.Parse("0,0,W"));
        }

        [Test]
        public void MoveWorksInAllDirections()
        {
            var plateau = new Plateau(1, 1);
            
            var position = new Position(0, 0, "N");
            var newPosition = position.MoveForward(plateau);
            Check.That(newPosition).IsEqualTo(Position.Parse("0,1,N"));

            position = new Position(0, 1, "S");
            newPosition = position.MoveForward(plateau);
            Check.That(newPosition).IsEqualTo(Position.Parse("0,0,S"));
            
            position = new Position(0, 1, "E");
            newPosition = position.MoveForward(plateau);
            Check.That(newPosition).IsEqualTo(Position.Parse("1,1,E"));

            position = new Position(1, 0, "W");
            newPosition = position.MoveForward(plateau);
            Check.That(newPosition).IsEqualTo(Position.Parse("0,0,W"));
        }

        [Test]
        public void MoveCanNotExitThePlateauWhateverTheDirection()
        {
            var plateau = new Plateau(1, 1);

            var position = new Position(0, 1, "N");
            var newPosition = position.MoveForward(plateau);
            Check.That(newPosition).IsEqualTo(Position.Parse("0,1,N"));

            position = new Position(0, 0, "S");
            newPosition = position.MoveForward(plateau);
            Check.That(newPosition).IsEqualTo(Position.Parse("0,0,S"));

            position = new Position(1, 1, "E");
            newPosition = position.MoveForward(plateau);
            Check.That(newPosition).IsEqualTo(Position.Parse("1,1,E"));

            position = new Position(0, 0, "W");
            newPosition = position.MoveForward(plateau);
            Check.That(newPosition).IsEqualTo(Position.Parse("0,0,W"));
        }

        [Test]
        public void ParseWorks()
        {
            var position = Position.Parse("2,3,N");

            Check.That(position.X).IsEqualTo(2);
            Check.That(position.Y).IsEqualTo(3);
            Check.That(position.CardinalCompassOrientation).IsEqualTo("N");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseThrowsWhenUnknownCardinalCompassOrientationValue()
        {
            var position = Position.Parse("2,3,X");
        }

        [Test]
        public void ToStringWorks()
        {
            var firstPosition = new Position(2, 3, "N");

            Check.That(firstPosition.ToString()).IsEqualTo("2,3,N");
        }
    }
}