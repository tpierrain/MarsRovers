namespace MarsRovers.Tests
{
    using NFluent;

    using NUnit.Framework;

    public class AcceptanceTests
    {
        [Test]
        public void FirstRoverArrivesWhereExpected()
        {
            var plateau = new Plateau(5, 5);
            var rover = new Rover();
            rover.Land(plateau, "1,2,N");
            rover.MoveWithIntructions("LMLMLMLMM");
            
            Check.That(rover.Position).IsEqualTo(Position.Parse("1,3,N"));
        }

        [Test]
        public void SecondRoverArrivesWhereExpectedToo()
        {
            var plateau = new Plateau(5, 5);

            var rover = new Rover();
            rover.Land(plateau, "1,2,N");
            rover.MoveWithIntructions("LMLMLMLMM");
            Check.That(rover.Position).IsEqualTo(Position.Parse("1,3,N"));

            var secondRover = new Rover();
            secondRover.Land(plateau, "3,3,E");
            secondRover.MoveWithIntructions("MMRMMRMRRM");
            Check.That(secondRover.Position).IsEqualTo(Position.Parse("5,1,E"));
        }
    }
}