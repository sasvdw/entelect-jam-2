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

        this.gamePresenter.HandleHorizontalInput(this.Player, horizontalInput);

        if(jumpButtonPressed)
        {
            this.gamePresenter.HandleJumpInput(this.Player);
        }
    }
}
