namespace MarsRovers
{
    using System;

    /// <summary>
    /// Describes a position.
    /// <remarks>
    ///     implement closure of operations
    /// </remarks>
    /// </summary>
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
            
            var cardinalCompassOrientationValue = splitedPattern[2];
            if (!"NSEW".Contains(cardinalCompassOrientationValue))
            {
                throw new ArgumentException("Unknown cardinal compass orientation value. Should be either: N, S, E or W");
            }
            
            return new Position(Convert.ToInt16(splitedPattern[0]), Convert.ToInt16(splitedPattern[1]), cardinalCompassOrientationValue);
        }

        public Position Move(Plateau plateau)
        {
            Position newPosition = this;

            if (this.IsNorthOriented() && this.DoesNotTouchTheNorthBundaries(plateau))
            {
                newPosition = new Position(this.X, this.Y + 1, this.CardinalCompassOrientation);
            }

            if (this.IsSouthOriented() && this.DoesNotTouchTheSouthBundaries(plateau))
            {
                newPosition = new Position(this.X, this.Y - 1, this.CardinalCompassOrientation);
            }

            if (this.IsEastOriented() && this.DoesNotTouchTheEastBundaries(plateau))
            {
                newPosition = new Position(this.X + 1, this.Y, this.CardinalCompassOrientation);
            }

            if (this.IsWestOriented() && this.DoesNotTouchTheWestBundaries(plateau))
            {
                newPosition = new Position(this.X - 1, this.Y, this.CardinalCompassOrientation);
            }

            return newPosition;
        }

        private bool DoesNotTouchTheWestBundaries(Plateau plateau)
        {
            return this.X > 0;
        }

        private bool DoesNotTouchTheEastBundaries(Plateau plateau)
        {
            return this.X < plateau.UpperRightCoordinatesX;
        }

        private bool IsEastOriented()
        {
            return this.CardinalCompassOrientation == "E";
        }

        private bool IsWestOriented()
        {
            return this.CardinalCompassOrientation == "W";
        }
        
        private bool IsNorthOriented()
        {
            return this.CardinalCompassOrientation == "N";
        }

        private bool IsSouthOriented()
        {
            return this.CardinalCompassOrientation == "S";
        }

        private bool DoesNotTouchTheNorthBundaries(Plateau plateau)
        {
            return this.Y < plateau.UpperRightCoordinatesY;
        }

        private bool DoesNotTouchTheSouthBundaries(Plateau plateau)
        {
            return this.Y > 0;
        }

        public Position TurnLeft()
        {
            switch (this.CardinalCompassOrientation)
            {
                case "N":
                    return new Position(this.X, this.Y, "W");

                case "W":
                    return new Position(this.X, this.Y, "S");

                case "S":
                    return new Position(this.X, this.Y, "E");

                case "E":
                    return new Position(this.X, this.Y, "N");
            }

            return this;
        }

        public Position TurnRight()
        {
            switch (this.CardinalCompassOrientation)
            {
                case "N":
                    return new Position(this.X, this.Y, "E");

                case "W":
                    return new Position(this.X, this.Y, "N");

                case "S":
                    return new Position(this.X, this.Y, "W");

                case "E":
                    return new Position(this.X, this.Y, "S");
            }

            return this;
        }
    }
}