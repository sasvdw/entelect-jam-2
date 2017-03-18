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
        private Player playerInRangeToKick;
        private float direction;
        private Player playerKickedBy;

        public float MoveSpeedMagnitude
        {
            get
            {
                return this.moveSpeed * this.direction;
            }
        }

        public Vector2 JumpForce
        {
            get
            {
                return Vector2.up * this.jumpModifier;
            }
        }

        public Vector2 KickForce
        {
            get
            {
                if(this.playerKickedBy == null)
                {
                    return Vector2.zero;
                }

                var verticalComponent = Vector2.up;
                var horizontalComponent = Vector2.right * this.playerKickedBy.direction;
                var kickForce = (verticalComponent + horizontalComponent) * this.playerKickedBy.kickModifier;

                return kickForce;
            }
        }

        public bool CanJump
        {
            get
            {
                return this.isGrounded && !this.wasKicked || this.canDoubleJump;
            }
        }

        public bool CanMove
        {
            get
            {
                return !this.wasKicked;
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
        public bool HadJumped { get; private set; }

        public Player()
        {
            this.moveSpeed = 10.0f;
            this.jumpModifier = 20.0f;
            this.kickModifier = 15.0f;
            this.playerInRangeToKick = null;
        }

        public void Landed()
        {
            this.isGrounded = true;
            this.canDoubleJump = true;
            this.wasKicked = false;
            this.HadJumped = false;
        }

        public void Jump()
        {
            if(!this.isGrounded)
            {
                this.canDoubleJump = false;
                this.HadJumped = true;
                this.wasKicked = false;
                return;
            }

            this.isGrounded = false;
            this.HadJumped = true;
        }

        public void Jumped()
        {
            this.HadJumped = false;
        }

        public void SetPlayerInRangeToKick(Player playerToKick)
        {
            this.playerInRangeToKick = playerToKick;
        }

        public void Kick()
        {
            if(this.playerInRangeToKick == null)
            {
                return;
            }

            this.playerInRangeToKick.playerKickedBy = this;
            this.playerInRangeToKick.wasKicked = true;
            this.playerInRangeToKick.isGrounded = false;
        }

        public void Kicked()
        {
            this.playerKickedBy = null;
        }

        public void SetPlayerOutRangeForKicking()
        {
            this.playerInRangeToKick = null;
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

        public void Moved()
        {
            this.WasMoving = false;
        }
    }
}
