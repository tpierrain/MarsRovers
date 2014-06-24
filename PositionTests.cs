namespace MarsRovers
{
    using NFluent;
    using NUnit.Framework;
    
    public class PositionTests
    {
        [Test]
        public void IsEqualPositionWorksByValue()
        {
            var firstPosition = new Position(2, 3, "N");
            var secondPosition = new Position(2, 3, "N");
            var nonEqualPosition = new Position(1, 3, "N");

            Check.That(secondPosition).IsEqualTo(firstPosition);
            Check.That(nonEqualPosition).IsNotEqualTo(firstPosition);
        }

        [Test]
        public void ToStringWorks()
        {
            var firstPosition = new Position(2, 3, "N");

            Check.That(firstPosition.ToString()).IsEqualTo("2,3,N");
        }
    }
}