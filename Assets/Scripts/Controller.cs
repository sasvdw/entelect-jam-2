using Assets.Scripts.Models;
using Assets.Scripts.Presenters;
using Rewired;
using UnityEngine;
using Player = Rewired.Player;

public class Controller : MonoBehaviour
{
    private IGamePresenter gamePresenter;
    public PlayerType Player;
    private Player player;

    // Use this for initialization
    private void Start()
    {
        this.gamePresenter = GamePresenter.Instance;
        this.player = ReInput.players.GetPlayer((int)this.Player);
    }

    // Update is called once per frame
    private void Update()
    {
        var horizontalInput = this.player.GetAxis("MoveHorizontal");
        var jumpButtonPressed = this.player.GetButtonDown("Jump");
        var kickButtonPressed = this.player.GetButtonDown("Kick");

        this.gamePresenter.HandleHorizontalInput(this.Player, horizontalInput);

        if(jumpButtonPressed)
        {
            this.gamePresenter.HandleJumpInput(this.Player);
        }

        if(kickButtonPressed)
        {
            this.gamePresenter.HandleKickInput(this.Player);
        }
    }
}
