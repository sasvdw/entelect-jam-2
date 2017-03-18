using Assets.Scripts.Models;
using Assets.Scripts.Presenters;
using Assets.Scripts.Views;
using UnityEngine;

public class Character : MonoBehaviour, IPlayerView
{
    private new Rigidbody2D rigidbody2D;
    private Vector2 jumpForce;
    private IGamePresenter gamePresenter;

    public PlayerType Player;

    public PlayerType PlayerType
    {
        get
        {
            return this.Player;
        }
    }

    private Vector2 VerticalVelocity
    {
        get
        {
            return Vector2.up * this.rigidbody2D.velocity.y;
        }
    }

    public void Move(float speed)
    {
        var moveSpeed = Vector2.right * speed;

        this.rigidbody2D.velocity = this.VerticalVelocity + moveSpeed;

        var directionModifier = Mathf.Sign(speed);
        this.transform.localScale = new Vector2(directionModifier, 1);
    }

    public void Stop()
    {
        this.rigidbody2D.velocity = this.VerticalVelocity + Vector2.zero;
    }

    public void Kick(Vector2 kickForce)
    {
        if(kickForce == Vector2.zero)
        {
            return;
        }

        this.rigidbody2D.velocity = Vector2.zero;
        this.rigidbody2D.AddForce(kickForce, ForceMode2D.Impulse);
    }

    public void Jump(Vector2 jumpForce)
    {
        this.rigidbody2D.AddForce(jumpForce, ForceMode2D.Impulse);
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
        this.gamePresenter.StepPlayerPhysics(this.Player);
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
