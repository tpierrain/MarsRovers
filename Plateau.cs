namespace MarsRovers
{
    using System;
    using System.Collections.Generic;

    public class Plateau
    {
        #region Fields

        private readonly Dictionary<Rover, Position> roversPositions;

        #endregion

        #region Constructors and Destructors

        public Plateau(int upperRightCoordinatesX, int upperRightCoordinatesY)
        {
            this.roversPositions = new Dictionary<Rover, Position>();
            this.UpperRightCoordinatesX = upperRightCoordinatesX;
            this.UpperRightCoordinatesY = upperRightCoordinatesY;
        }

        #endregion

        #region Public Properties

        public int UpperRightCoordinatesX { get; private set; }

        public int UpperRightCoordinatesY { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void LandRoverAtPosition(Rover rover, Position landingPosition)
        {
            if (this.AlreadyOccupied(landingPosition))
            {
                throw new InvalidOperationException();
            }

            UpdateRoverPosition(rover, landingPosition);
        }

        private bool AlreadyOccupied(Position landingPosition)
        {
            bool alreadyOccupied = false;
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

        public void UpdateRoverPosition(Rover rover, Position newPosition)
        {
            this.roversPositions[rover] = newPosition;
        }
    }
}