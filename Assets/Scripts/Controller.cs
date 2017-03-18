using Assets.Scripts.Models;
using Assets.Scripts.Presenters;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private IGamePresenter gamePresenter;
    public PlayerType Player;

    // Use this for initialization
    private void Start()
    {
        this.gamePresenter = GamePresenter.Instance;
    }

    // Update is called once per frame
    private void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var jumpButtonPressed = Input.GetButtonDown("Jump");
        var fire1ButtonPressed = Input.GetButtonDown("Fire1");

        this.gamePresenter.HandleHorizontalInput(this.Player, horizontalInput);

        if(jumpButtonPressed)
        {
            this.gamePresenter.HandleJumpInput(this.Player);
        }

        if(fire1ButtonPressed)
        {
            this.gamePresenter.HandleKickInput(this.Player);
        }
    }
}
