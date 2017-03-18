using Assets.Scripts.Presenters;
using UnityEngine;

public class KickCheck : MonoBehaviour
{
    private Character character;
    private IGamePresenter gamePresenter;
    // Use this for initialization
    void Start()
    {
        this.character = this.gameObject.GetComponentInParent<Character>();
        this.gamePresenter = GamePresenter.Instance;
    }

    // Update is called once per frame
    void Update() {}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.isTrigger)
        {
            return;
        }

        var otherCharacter = other.gameObject.GetComponent<Character>();

        if(!otherCharacter)
        {
            return;
        }

        this.gamePresenter.PlayerInRangeForKick(this.character.Player, otherCharacter.Player);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.isTrigger)
        {
            return;
        }

        var otherCharacter = other.gameObject.GetComponent<Character>();

        if(!otherCharacter)
        {
            return;
        }

        this.gamePresenter.PlayerOutRangeForKick(this.character.Player);
    }
}
