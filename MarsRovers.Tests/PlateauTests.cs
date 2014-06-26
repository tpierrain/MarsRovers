namespace MarsRovers.Tests
{
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
    }
}
