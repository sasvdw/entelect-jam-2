using Assets.Scripts.Models;
using Assets.Scripts.Presenters;
using Assets.Scripts.Views;
using UnityEngine;

public class Character : MonoBehaviour, IPlayerView
{
    private new Rigidbody2D rigidbody2D;
    private Vector2 moveSpeed;
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

    public void Move(float speed)
    {
        this.moveSpeed = new Vector2(speed, this.rigidbody2D.velocity.y);

        var directionModifier = speed / Mathf.Abs(speed);
        this.transform.localScale = new Vector2(directionModifier, 1);
    }

    public void Stop()
    {
        this.moveSpeed = new Vector2(0.0f, this.rigidbody2D.velocity.y);
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
        this.rigidbody2D.velocity = moveSpeed;

        this.rigidbody2D.AddForce(this.jumpForce, ForceMode2D.Impulse);
        this.jumpForce = Vector2.zero;
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
