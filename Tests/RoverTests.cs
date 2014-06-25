namespace MarsRovers.Tests
{
    using NFluent;
    using NUnit.Framework;

    [TestFixture]
    public class RoverTests
    {
        private readonly AcceptanceTests acceptanceTests = new AcceptanceTests();

        [Test]
        public void LandWorks()
        {
            var plateau = new Plateau(1, 1);
            var rover = new Rover();
            
            rover.Land(plateau, "0,1,N");
            Check.That(rover.Position).IsEqualTo(Position.Parse("0,1,N"));
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
    }
}
