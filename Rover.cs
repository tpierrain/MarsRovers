namespace MarsRovers
{
    using System;

    /// <summary>
    /// Remote vehicule with on-board cameras that can get a complete view of the surrounding terrain to send back to Earth (from Mars).
    /// </summary>
    public class Rover
    {
        private Plateau plateau;

        public void Land(Plateau plateau, string positionPattern)
        {
            this.plateau = plateau;
            this.Position = Position.Parse(positionPattern);
            // TODO: throws if position pattern is outside the plateau's dimension
            this.plateau.LandRoverAtPosition(this, this.Position);
        }

        /// <summary>
        /// Moves the rover following the intructions (a list of M, L and R letters).
        /// </summary>
        /// <param name="moveInstructions">The move instructions.</param>
        /// <exception cref="System.InvalidOperationException">Unknown instruction. Should be either M, L or R letters (for Move forward, Turn Left or Turn Right)</exception>
        public void MoveWithIntructions(string moveInstructions)
        {
            foreach (var moveInstruction in moveInstructions)
            {
                Position candidateNewPosition;
                switch (moveInstruction)
                {
                    case 'M':
                        candidateNewPosition = this.Position.MoveOneStepForward();
                        this.CommitMoveInstructionIfAllowed(candidateNewPosition);
                        break;

                    case 'L':
                        candidateNewPosition = this.Position.TurnLeft();
                        this.CommitMoveInstructionIfAllowed(candidateNewPosition);
                        break;

                    case 'R':
                        candidateNewPosition = this.Position.TurnRight();
                        this.CommitMoveInstructionIfAllowed(candidateNewPosition);
                        break;

                    default:
                        throw new InvalidOperationException(string.Format("Unknown instruction: {0}. Should be either M, L or R letters (for Move forward, Turn Left or Turn Right)", moveInstruction));
                }
            }
        }

        private void CommitMoveInstructionIfAllowed(Position candidateNewPosition)
        {
            if (this.plateau.CanSupport(candidateNewPosition))
            {
                this.plateau.UpdateRoverPosition(this, candidateNewPosition);
                this.Position = candidateNewPosition;
            }
        }

        public Position Position { get; private set; }
    }
}