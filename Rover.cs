namespace MarsRovers
{
    public class Rover
    {
        private Plateau plateau;

        public void Land(Plateau plateau, string positionPattern)
        {
            this.plateau = plateau;
            this.Position = Position.Parse(positionPattern);
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
                        newPosition = this.Position.Move(this.plateau);
                        this.plateau.UpdateRoverPosition(this, newPosition);
                        this.Position = newPosition;
                        break;

                    case 'L':
                        newPosition = this.Position.TurnLeft();
                        this.plateau.UpdateRoverPosition(this, newPosition);
                        this.Position = newPosition;
                        break;
                    default:
                        break;
                }
            }
        }

        public Position Position { get; private set; }
    }
}