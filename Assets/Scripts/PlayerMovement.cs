using Assets.Scripts.Models;
using Assets.Scripts.Presenters;
using Assets.Scripts.Views;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerView
{
    private new Rigidbody2D rigidbody2D;
    private int collidersStandingOn;
    private Vector2 moveSpeed;
    private Vector2 jumpForce;

    public PlayerType PlayerType
    {
        get
        {
            return PlayerType.One;
        }
    }

    public PlayerMovement()
    {
        GamePresenter.Instance.AddPlayer(this);
    }

    public void Move(float speed)
    {
        this.moveSpeed = new Vector2(speed, this.rigidbody2D.velocity.y);
    }

    public void Jump(float jumpModifier)
    {
        this.jumpForce = Vector2.up * jumpModifier;
    }

    // Use this for initialization
    private void Start()
    {
        this.rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
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
        this.collidersStandingOn++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.isTrigger)
        {
            return;
        }
        this.collidersStandingOn--;
    }
}
