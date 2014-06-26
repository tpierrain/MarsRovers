namespace MarsRovers.Tests
{
    using System;

    using NFluent;

    using NUnit.Framework;

    [TestFixture]
    public class PlateauTests
    {
        [Test]
        public void CanSupportWorks()
        {
            var plateau = new Plateau("5,5");

            Check.That(plateau.CanSupport(Position.Parse("0,0,S"))).IsTrue();
            Check.That(plateau.CanSupport(Position.Parse("1,2,N"))).IsTrue();
            Check.That(plateau.CanSupport(Position.Parse("5,5,E"))).IsTrue();
            
            Check.That(plateau.CanSupport(Position.Parse("5,6,W"))).IsFalse();
            Check.That(plateau.CanSupport(Position.Parse("6,0,S"))).IsFalse();
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
    }
}
