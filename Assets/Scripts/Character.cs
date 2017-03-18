using Assets.Scripts.Models;
using Assets.Scripts.Presenters;
using Assets.Scripts.Views;
using UnityEngine;

public class Character : MonoBehaviour, IPlayerView
{
    private new Rigidbody2D rigidbody2D;
    private Vector2 moveSpeed;
    private Vector2 jumpForce;
    private Vector2 kickForce;
    private IGamePresenter gamePresenter;

    public PlayerType Player;

    public PlayerType PlayerType
    {
        get
        {
            return this.Player;
        }
    }

    private float facingDirection
    {
        get
        {
            return this.transform.localScale.x;
        }
    }

    public void Move(float speed)
    {
        this.moveSpeed = Vector2.right * speed;

        var directionModifier = Mathf.Sign(speed);
        this.transform.localScale = new Vector2(directionModifier, 1);
    }

    public void Stop()
    {
        this.moveSpeed = Vector2.zero;
    }

    public void Kick(float playerKickMagnitude)
    {
        var verticalComponent = Vector2.up * Mathf.Abs(playerKickMagnitude);
        var horizontalComponent = Vector2.right * playerKickMagnitude;
        this.kickForce = (verticalComponent + horizontalComponent) / 2;
    }

    public void Jump(float jumpModifier)
    {
        this.jumpForce = Vector2.up * jumpModifier;
    }

    // Use this for initialization
    private void Start()
    {
        GamePresenter.Instance.AddPlayer(this);
        this.rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        this.gamePresenter = GamePresenter.Instance;
    }

    // Update is called once per frame
    private void Update() {}

    //FixedUpdate is called every fixed framerate frame
    private void FixedUpdate()
    {
        if(this.kickForce == Vector2.zero)
        {
            var verticalVelocity = Vector2.up * this.rigidbody2D.velocity.y;
            this.rigidbody2D.velocity = verticalVelocity + this.moveSpeed;
        }

        if(this.jumpForce != Vector2.zero)
        {
            this.rigidbody2D.AddForce(this.jumpForce, ForceMode2D.Impulse);
            this.jumpForce = Vector2.zero;
        }

        if (this.kickForce != Vector2.zero)
        {
            this.rigidbody2D.AddForce(this.kickForce, ForceMode2D.Impulse);
            this.moveSpeed = Vector2.right * this.kickForce.x;
            this.kickForce = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.isTrigger)
        {
            return;
        }
        this.gamePresenter.PlayerLanded(this.PlayerType);
    }
}
