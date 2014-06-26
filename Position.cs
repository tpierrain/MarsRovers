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
        /// <summary>
        /// Gets the x position on the plateau.
        /// </summary>
        /// <value>
        /// The x position on the plateau.
        /// </value>
        public int X { get; private set; }

        /// <summary>
        /// Gets the y position on the plateau.
        /// </summary>
        /// <value>
        /// The y position on the plateau.
        /// </value>
        public int Y { get; private set; }

        /// <summary>
        /// Gets the cardinal compass orientation.
        /// </summary>
        /// <value>
        /// The cardinal compass orientation.
        /// </value>
        public string CardinalCompassOrientation { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="cardinalCompassOrientation">The cardinal compass orientation.</param>
        public Position(int x, int y, string cardinalCompassOrientation)
        {
            this.X = x;
            this.Y = y;
            this.CardinalCompassOrientation = cardinalCompassOrientation;
        }
        
        #region overrides related

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0},{1},{2}", this.X, this.Y, this.CardinalCompassOrientation);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        protected bool Equals(Position other)
        {
            return this.X == other.X && this.Y == other.Y && string.Equals(this.CardinalCompassOrientation, other.CardinalCompassOrientation);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" }, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
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

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
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

        #endregion

        /// <summary>
        /// Parses the specified position pattern.
        /// </summary>
        /// <param name="positionPattern">The position pattern.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Unknown cardinal compass orientation value. Should be either: N, S, E or W</exception>
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

        /// <summary>
        /// Moves one step forward.
        /// </summary>
        /// <returns>The next position after we have moved one step forward.</returns>
        public Position MoveOneStepForward()
        {
            Position newPosition = this;

            if (this.IsNorthOriented())
            {
                newPosition = new Position(this.X, this.Y + 1, this.CardinalCompassOrientation);
            }

            if (this.IsSouthOriented() && this.Y > 0)
            {
                newPosition = new Position(this.X, this.Y - 1, this.CardinalCompassOrientation);
            }

            if (this.IsEastOriented())
            {
                newPosition = new Position(this.X + 1, this.Y, this.CardinalCompassOrientation);
            }

            if (this.IsWestOriented() && this.X > 0)
            {
                newPosition = new Position(this.X - 1, this.Y, this.CardinalCompassOrientation);
            }

            return newPosition;
        }

        /// <summary>
        /// Turns on the left.
        /// </summary>
        /// <returns>The next position after we have turned on the left.</returns>
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
                
                default:
                    return this;
            }
        }

        /// <summary>
        /// Turns on the right direction.
        /// </summary>
        /// <returns>The next position after we have turned on the right direction.</returns>
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

                default:
                    return this;
            }
        }

        #region private methods

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

        #endregion

    }
}