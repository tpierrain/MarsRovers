namespace MarsRovers.Tests
{
    using System;
    using NFluent;
    using NUnit.Framework;

    [TestFixture]
    public class RoverTests
    {
        [Test]
        public void LandWorks()
        {
            var plateau = new Plateau(1, 1);
            var rover = new Rover();
            
            rover.Land(plateau, "0,1,N");
            Check.That(rover.Position).IsEqualTo(Position.Parse("0,1,N"));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Position already occupied 0,1,N. Can not land.")]
        public void LandingOnAnotherRoverThrowsAnException()
        {
            var plateau = new Plateau(1, 1);
            var firstRover = new Rover();
            firstRover.Land(plateau, "0,1,N");

            var secondRover = new Rover();
            secondRover.Land(plateau, "0,1,N");
        }

        [Test]
        public void MoveStepByStep()
        {
            var plateau = new Plateau(1, 1);
            var rover = new Rover();
            rover.Land(plateau, "0,0,N");
            
            rover.MoveWithIntructions("M");
            Check.That(rover.Position).IsEqualTo(Position.Parse("0,1,N"));
        }

        [Test]
        public void CanNotExitThePlateau()
        {
            var plateau = new Plateau(1, 1);
            var rover = new Rover();
            
            rover.Land(plateau, "0,1,N");
            Check.That(rover.Position).IsEqualTo(Position.Parse("0,1,N"));
            
            rover.MoveWithIntructions("M");
            Check.That(rover.Position).IsEqualTo(Position.Parse("0,1,N"));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Unknown instruction: Z. Should be either M, L or R letters (for Move forward, Turn Left or Turn Right)")]
        public void MoveWithIntructionsThrowsWithUnknownInstruction()
        {
            var plateau = new Plateau(1, 1);
            var rover = new Rover();

            rover.Land(plateau, "0,1,N");
            rover.MoveWithIntructions("MLZ");
        }

        [Test]
        public void CanNotMoveWhenOtherRoverIsBehindUs()
        {
            var plateau = new Plateau(2, 2);
            var firstRover = new Rover();

            firstRover.Land(plateau, "0,1,N");
            Check.That(firstRover.Position).IsEqualTo(Position.Parse("0,1,N"));
            
            firstRover.MoveWithIntructions("M");
            Check.That(firstRover.Position).IsEqualTo(Position.Parse("0,2,N"));

            var secondRover = new Rover();
            secondRover.Land(plateau, "0,1,N");
            Check.That(secondRover.Position).IsEqualTo(Position.Parse("0,1,N"));

            secondRover.MoveWithIntructions("M");
            Check.That(secondRover.Position).IsEqualTo(Position.Parse("0,1,N"));
        }
    }
}
