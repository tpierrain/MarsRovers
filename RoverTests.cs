namespace MarsRovers
{
    using System.Security.Cryptography.X509Certificates;

    using NFluent;

    using NUnit.Framework;

    [TestFixture]
    public class RoverTests
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
    }

    public class Rover
    {
        private Plateau plateau;

        public void Land(Plateau plateau, string positionPattern)
        {
            this.plateau = plateau;
            var position = Position.Parse(positionPattern);
            this.Land(position.X, position.Y, position.CardinalCompassOrientation);
        }

        public void Land(int xCoordinate, int yCoordinate, string cardinalCompassOrientation)
        {
            this.plateau.LandRoverAtPosition(this, xCoordinate, yCoordinate, cardinalCompassOrientation);
        }

        public void MoveWithIntructions(string moveInstructions)
        {
        }

        public Position Position { get; private set; }
    }

    public class Plateau
    {
        private int upperRightCoordinates;
        private int lowerLeftCoordinates;

        public Plateau(int upperRightCoordinates, int lowerLeftCoordinates)
        {
            this.upperRightCoordinates = upperRightCoordinates;
            this.lowerLeftCoordinates = lowerLeftCoordinates;
        }

        public void LandRoverAtPosition(Rover rover, int xCoordinate, int yCoordinate, string cardinalCompassOrientation)
        {
            
        }
    }
}
