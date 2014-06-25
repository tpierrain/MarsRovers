namespace MarsRovers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Surface where Rovers may land and move.
    /// </summary>
    public class Plateau
    {
        #region Fields

        private readonly Dictionary<Rover, Position> roversPositions;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Plateau"/> class.
        /// </summary>
        /// <param name="upperRightCoordinates">The upper right coordinates.</param>
        public Plateau(string upperRightCoordinates)
        {
            var splitedCoordinates = upperRightCoordinates.Split(',');
            this.roversPositions = new Dictionary<Rover, Position>();
            this.UpperRightCoordinatesX = Convert.ToInt16(splitedCoordinates[0]);
            this.UpperRightCoordinatesY = Convert.ToInt16(splitedCoordinates[1]);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plateau"/> class.
        /// </summary>
        /// <param name="upperRightCoordinatesX">The upper right coordinates x.</param>
        /// <param name="upperRightCoordinatesY">The upper right coordinates y.</param>
        public Plateau(int upperRightCoordinatesX, int upperRightCoordinatesY)
        {
            this.roversPositions = new Dictionary<Rover, Position>();
            this.UpperRightCoordinatesX = upperRightCoordinatesX;
            this.UpperRightCoordinatesY = upperRightCoordinatesY;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the x position of the upper right coordinates.
        /// </summary>
        /// <value>
        /// The x position of the upper right coordinates.
        /// </value>
        public int UpperRightCoordinatesX { get; private set; }

        /// <summary>
        /// Gets the y position of the upper right coordinates.
        /// </summary>
        /// <value>
        /// The y position of the upper right coordinates.
        /// </value>
        public int UpperRightCoordinatesY { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Lands a rover at a given position.
        /// </summary>
        /// <param name="rover">The rover to land.</param>
        /// <param name="landingPosition">The landing position.</param>
        /// <exception cref="System.InvalidOperationException">The position is already occupied by another Rover.</exception>
        public void LandRoverAtPosition(Rover rover, Position landingPosition)
        {
            if (this.IsAlreadyOccupied(landingPosition))
            {
                throw new InvalidOperationException(string.Format("Position already occupied {0}. Can not land.", landingPosition));
            }

            UpdateRoverPosition(rover, landingPosition);
        }

        /// <summary>
        /// Determines whether or not the specified landing position is already occupied.
        /// </summary>
        /// <param name="landingPosition">The landing position.</param>
        /// <returns>true if the specified landing position is already occupied; false otherwise.</returns>
        private bool IsAlreadyOccupied(Position landingPosition)
        {
            var alreadyOccupied = false;
            
            foreach (var occupiedPosition in this.roversPositions.Values)
            {
                if (landingPosition == occupiedPosition)
                {
                    alreadyOccupied = true;
                }
            }

            return alreadyOccupied;
        }

        #endregion

        /// <summary>
        /// Updates the position of a given Rover without executing any check (on the boundary or on a possible occupation).
        /// </summary>
        /// <param name="rover">The rover to be updated.</param>
        /// <param name="newPosition">The new position for the rover.</param>
        public void UpdateRoverPosition(Rover rover, Position newPosition)
        {
            this.roversPositions[rover] = newPosition;
        }
    }
}