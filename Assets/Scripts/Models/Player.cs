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

        private bool isGrounded;
        private bool canDoubleJump;

        public bool CanJump
        {
            get
            {
                return this.isGrounded || this.canDoubleJump;
            }
        }

        public Player()
        {
            this.moveSpeed = 10.0f;
            this.jumpModifier = 20.0f;
        }

        public void Landed()
        {
            this.isGrounded = true;
            this.canDoubleJump = true;
        }

        public void Jumped()
        {
            if(!this.isGrounded)
            {
                this.canDoubleJump = false;
                return;
            }

            this.isGrounded = false;
        }
    }
}
