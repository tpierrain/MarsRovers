namespace MarsRovers
{
    using System;

    public class Position
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public string CardinalCompassOrientation { get; private set; }

        public Position(int x, int y, string cardinalCompassOrientation)
        {
            this.X = x;
            this.Y = y;
            this.CardinalCompassOrientation = cardinalCompassOrientation;
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", this.X, this.Y, this.CardinalCompassOrientation);
        }

        protected bool Equals(Position other)
        {
            return this.X == other.X && this.Y == other.Y && string.Equals(this.CardinalCompassOrientation, other.CardinalCompassOrientation);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return this.Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.X;
                hashCode = (hashCode * 397) ^ this.Y;
                hashCode = (hashCode * 397) ^ (this.CardinalCompassOrientation != null ? this.CardinalCompassOrientation.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Position left, Position right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !Equals(left, right);
        }

        public static Position Parse(string positionPattern)
        {
            var splitedPattern = positionPattern.Split(',');
            return new Position(Convert.ToInt16(splitedPattern[0]), Convert.ToInt16(splitedPattern[1]), splitedPattern[2]);
        }
    }
}