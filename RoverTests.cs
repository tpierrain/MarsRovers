namespace MarsRovers
{
    using System.Security.Cryptography.X509Certificates;

    using NFluent;

    using NUnit.Framework;

    [TestFixture]
    public class RoverTests
    {
        [Test]
        public void FirstRoverTest()
        {
            var plateau = new Plateau(5, 5);
            var rover = new Rover(plateau);
            rover.LandRoverAtPosition(1, 2, "N");
            rover.MoveWithIntructions("LMLMLMLMM");
            Check.That(rover.Position).IsEqualTo(new Position(1, 3, "N"));
        }
    }

    public class Rover
    {
        private Plateau plateau;

        public Rover(Plateau plateau)
        {
            this.plateau = plateau;
        }

        public void LandRoverAtPosition(int xCoordinate, int yCoordinate, string cardinalCompassOrientation)
        {
        }

        public void MoveWithIntructions(string moveInstructions)
        {
        }

        public Position Position { get; private set; }
    }

    public class Plateau
    {
        public Plateau(int upperRightCoordinates, int lowerLeftCoordinates)
        {
        }
    }
}
