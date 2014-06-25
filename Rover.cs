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
                Position newPosition;
                switch (moveInstruction)
                {
                    case 'M':
                        newPosition = this.Position.MoveOneStepForward(this.plateau);
                        this.plateau.UpdateRoverPosition(this, newPosition);
                        this.Position = newPosition;
                        break;

                    case 'L':
                        newPosition = this.Position.TurnLeft();
                        this.plateau.UpdateRoverPosition(this, newPosition);
                        this.Position = newPosition;
                        break;

                    case 'R':
                        newPosition = this.Position.TurnRight();
                        this.plateau.UpdateRoverPosition(this, newPosition);
                        this.Position = newPosition;
                        break;
                    default:
                        throw new InvalidOperationException(string.Format("Unknown instruction: {0}", moveInstruction));
                        break;
                }
            }
        }

        public Position Position { get; private set; }
    }
}