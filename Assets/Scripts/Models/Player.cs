namespace Assets.Scripts.Models
{
    public class Player
    {
        private readonly float moveSpeed;
        private readonly float jumpModifier;

        public float MoveSpeed
        {
            get
            {
                return this.moveSpeed;
            }
        }

        public float JumpModifier
        {
            get
            {
                return this.jumpModifier;
            }
        }

        public Player()
        {
            this.moveSpeed = 10.0f;
            this.jumpModifier = 20.0f;
        }
    }
}
