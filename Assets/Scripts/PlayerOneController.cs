using Assets.Scripts.Models;
using Assets.Scripts.Presenters;
using UnityEngine;

public class PlayerOneController : MonoBehaviour
{
    private readonly IGamePresenter gamePresenter;

    public PlayerOneController()
    {
        this.gamePresenter = GamePresenter.Instance;
    }

    // Use this for initialization
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var jumpButtonPressed = Input.GetButtonDown("Jump");

        this.gamePresenter.HandleHorizontalInput(PlayerType.One, horizontalInput);
        if(jumpButtonPressed)
        {
            this.gamePresenter.HandleJumpInput(PlayerType.One);
        }
    }
}
