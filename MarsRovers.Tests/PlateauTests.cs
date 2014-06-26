namespace MarsRovers.Tests
{
    using System;
    using NFluent;
    using NUnit.Framework;

    [TestFixture]
    public class PlateauTests
    {
        [Test]
        public void HasTopologySupportingIndicatesIfPositionIsPartOfThePlateau()
        {
            var plateau = new Plateau("5,5");

            Check.That(plateau.HasTopologySupporting(Position.Parse("0,0,S"))).IsTrue();
            Check.That(plateau.HasTopologySupporting(Position.Parse("1,2,N"))).IsTrue();
            Check.That(plateau.HasTopologySupporting(Position.Parse("5,5,E"))).IsTrue();
            
            Check.That(plateau.HasTopologySupporting(Position.Parse("5,6,W"))).IsFalse();
            Check.That(plateau.HasTopologySupporting(Position.Parse("6,0,S"))).IsFalse();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Can not land out of the plateau bundaries.\nRequested landing position: [6,6,S].\nCurrent plateau boundaries: [5,5].")]
        public void LandRoverOutsideThePlateauThrowsAnException()
        {
            var plateau = new Plateau("5,5");
            var rover = new Rover();

            plateau.LandRoverAtPosition(rover, Position.Parse("6,6,S"));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Can not land out of the plateau bundaries.\nRequested landing position: [6,-1,S].\nCurrent plateau boundaries: [5,5].")]
        public void LandRoverOutsideThePlateauSubZeroThrowsAnException()
        {
            var plateau = new Plateau("5,5");
            var rover = new Rover();

            plateau.LandRoverAtPosition(rover, Position.Parse("6,-1,S"));
        }

        [Test]
        public void HasARoverAlreadyWorks()
        {
            var plateau = new Plateau("5,5");
            var rover = new Rover();

            plateau.LandRoverAtPosition(rover, Position.Parse("3,2,S"));

            Check.That(plateau.HasARoverAlready(Position.Parse("3,2,S"))).IsTrue();
            Check.That(plateau.HasARoverAlready(Position.Parse("3,2,N"))).IsTrue();
        }
    }
}
