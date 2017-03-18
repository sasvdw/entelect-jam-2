using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Player
    {
        private readonly float moveSpeed;
        private readonly float jumpModifier;
        private readonly float kickModifier;

        private bool isGrounded;
        private bool canDoubleJump;
        private bool wasKicked;
        private PlayerType playerInRangeToKick;
        private float direction;

        public float MoveSpeedMagnitude
        {
            get
            {
                return this.moveSpeed * this.direction;
            }
        }

        public float JumpMagnitude
        {
            get
            {
                return this.jumpModifier;
            }
        }

        public float KickMagnitude
        {
            get
            {
                return this.kickModifier * this.direction;
            }
        }

        public bool CanJump
        {
            get
            {
                return this.isGrounded || this.canDoubleJump;
            }
        }

        public bool WasKicked
        {
            get
            {
                return this.wasKicked;
            }
        }

        public bool WasMoving { get; private set; }

        public Player()
        {
            this.moveSpeed = 10.0f;
            this.jumpModifier = 20.0f;
            this.kickModifier = 20.0f;
            this.playerInRangeToKick = (PlayerType)(-1);
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

        public void SetPlayerInRangeToKick(PlayerType playerToKick)
        {
            this.playerInRangeToKick = playerToKick;
        }

        public PlayerType Kick()
        {
            return this.playerInRangeToKick;
        }

        public void SetPlayerOutRangeForKicking()
        {
            this.playerInRangeToKick = (PlayerType)(-1);
        }

        public void StartedMoving(float input)
        {
            this.direction = Mathf.Sign(input);
            this.WasMoving = true;
        }

        public void StopedMoving()
        {
            this.WasMoving = false;
        }
    }
}
