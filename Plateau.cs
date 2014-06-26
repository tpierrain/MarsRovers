namespace MarsRovers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Field where Rovers may land and move. 
    /// The <see cref="Plateau"/> is responsible to provide informations about its topology and what is on it.
    /// </summary>
    public class Plateau
    {
        #region Fields

        private readonly Dictionary<Rover, Position> roversOnTheFieldWithTheirPositions;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Plateau"/> class.
        /// </summary>
        /// <param name="upperRightCoordinates">The upper right coordinates.</param>
        public Plateau(string upperRightCoordinates)
        {
            var splitedCoordinates = upperRightCoordinates.Split(',');
            this.roversOnTheFieldWithTheirPositions = new Dictionary<Rover, Position>();
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
            this.roversOnTheFieldWithTheirPositions = new Dictionary<Rover, Position>();
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
        /// <exception cref="System.InvalidOperationException">The position is already occupied by another Rover or the requested landing position is out of the plateau boundaries.</exception>
        public void LandRoverAtPosition(Rover rover, Position landingPosition)
        {
            if (this.IsAlreadyOccupied(landingPosition))
            {
                throw new InvalidOperationException(string.Format("Position already occupied {0}. Can not land.", landingPosition));
            }

            if (!this.CanSupport(landingPosition))
            {
                throw new InvalidOperationException(string.Format("Can not land out of the plateau bundaries.\nRequested landing position: [{0}].\nCurrent plateau boundaries: [{1},{2}].", landingPosition, this.UpperRightCoordinatesX, this.UpperRightCoordinatesY));
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
            
            foreach (var occupiedPosition in this.roversOnTheFieldWithTheirPositions.Values)
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
            this.roversOnTheFieldWithTheirPositions[rover] = newPosition;
        }

        /// <summary>
        /// Determines whether this position is part of this plateau instance or not.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>True is this position is part of this plateau; false otherwise.</returns>
        public bool CanSupport(Position position)
        {
            if ((0 <= position.X) && (position.X <= this.UpperRightCoordinatesX) && (0 <= position.Y) && (position.Y <= this.UpperRightCoordinatesY))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether there is no rover already to the specified position.
        /// </summary>
        /// <param name="specifiedPosition">The specified position.</param>
        /// <returns>true if no Rover is already at that position; false otherwise.</returns>
        public bool HasARoverAlready(Position specifiedPosition)
        {
            foreach (var position in this.roversOnTheFieldWithTheirPositions.Values)
            {
                if (position.HasSameCoordinates(specifiedPosition))
                {
                    return true;
                }
            }

            return false;
        }
    }
}