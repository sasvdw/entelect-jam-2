using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public interface IPlayerView
    {
        PlayerType PlayerType { get; }

        void Move(float speed);

        void Jump(Vector2 jumpModifier);

        void Stop();

        void Kick(Vector2 kickForce);
    }
}