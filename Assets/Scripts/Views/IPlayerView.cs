using Assets.Scripts.Models;

namespace Assets.Scripts.Views
{
    public interface IPlayerView
    {
        PlayerType PlayerType { get; }

        void Move(float speed);

        void Jump(float jumpModifier);

        void Stop();
    }
}