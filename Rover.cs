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

        public void MoveWithIntructions(string moveInstructions)
        {
            foreach (var moveInstruction in moveInstructions)
            {
                Position candidateNewPosition;
                switch (moveInstruction)
                {
                    case 'M':
                        candidateNewPosition = this.Position.MoveOneStepForward();
                        if (this.plateau.CanSupport(candidateNewPosition))
                        {
                            this.plateau.UpdateRoverPosition(this, candidateNewPosition);
                            this.Position = candidateNewPosition;
                        }
                        break;

                    case 'L':
                        candidateNewPosition = this.Position.TurnLeft();
                        if (this.plateau.CanSupport(candidateNewPosition))
                        {
                            this.plateau.UpdateRoverPosition(this, candidateNewPosition);
                            this.Position = candidateNewPosition;
                        }
                        break;

                    case 'R':
                        candidateNewPosition = this.Position.TurnRight();
                        if (this.plateau.CanSupport(candidateNewPosition))
                        {
                            this.plateau.UpdateRoverPosition(this, candidateNewPosition);
                            this.Position = candidateNewPosition;
                        }
                        break;

                    default:
                        throw new InvalidOperationException(string.Format("Unknown instruction: {0}", moveInstruction));
                }
            }
        }

        public Position Position { get; private set; }
    }
}