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
        public void MoveOneStepForwardWorksInAllDirections()
        {
            var position = new Position(0, 0, "N");
            var newPosition = position.MoveOneStepForward();
            Check.That(newPosition).IsEqualTo(Position.Parse("0,1,N"));

            position = new Position(0, 1, "S");
            newPosition = position.MoveOneStepForward();
            Check.That(newPosition).IsEqualTo(Position.Parse("0,0,S"));
            
            position = new Position(0, 1, "E");
            newPosition = position.MoveOneStepForward();
            Check.That(newPosition).IsEqualTo(Position.Parse("1,1,E"));

            position = new Position(1, 0, "W");
            newPosition = position.MoveOneStepForward();
            Check.That(newPosition).IsEqualTo(Position.Parse("0,0,W"));
        }

        [Test]
        public void MoveOneStepForwardCanNotExitThePlateauWhateverTheDirection()
        {
            var position = new Position(0, 1, "N");
            var newPosition = position.MoveOneStepForward();
            Check.That(newPosition).IsEqualTo(Position.Parse("0,2,N"));

            position = new Position(0, 0, "S");
            newPosition = position.MoveOneStepForward();
            Check.That(newPosition).IsEqualTo(Position.Parse("0,0,S"));

            position = new Position(1, 1, "E");
            newPosition = position.MoveOneStepForward();
            Check.That(newPosition).IsEqualTo(Position.Parse("2,1,E"));

            position = new Position(0, 0, "W");
            newPosition = position.MoveOneStepForward();
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
        public void HasSameCoordinatesWorks()
        {
            var position = Position.Parse("2,3,N");
            var samePosition = Position.Parse("2,3,E");

            Check.That(position.HasSameCoordinates(samePosition)).IsTrue();
            Check.That(position.HasSameCoordinates(position)).IsTrue();

            var positionWithDifferentCoordinates = Position.Parse("1,3,N");
            Check.That(position.HasSameCoordinates(positionWithDifferentCoordinates)).IsFalse();
        }

        [Test]
        public void ToStringWorks()
        {
            var firstPosition = new Position(2, 3, "N");

            Check.That(firstPosition.ToString()).IsEqualTo("2,3,N");
        }
    }
}