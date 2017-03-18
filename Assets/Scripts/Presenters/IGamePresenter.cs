using System.Collections.Generic;
using Assets.Scripts.Models;
using Assets.Scripts.Views;

namespace Assets.Scripts.Presenters
{
    public interface IGamePresenter
    {
        void AddPlayer(IPlayerView playerView);

        void HandleHorizontalInput(PlayerType playerType, float input);

        void HandleJumpInput(PlayerType playerType);
    }

    public class GamePresenter : IGamePresenter
    {
        private static readonly IGamePresenter instance;

        public static IGamePresenter Instance
        {
            get
            {
                return instance;
            }
        }

        private readonly IDictionary<PlayerType, IPlayerView> playerViews;
        private readonly Dictionary<PlayerType, Player> players;

        public GamePresenter()
        {
            this.playerViews = new Dictionary<PlayerType, IPlayerView>();
            this.players = new Dictionary<PlayerType, Player>();
        }

        static GamePresenter()
        {
            instance = new GamePresenter();
        }

        public void AddPlayer(IPlayerView playerView)
        {
            this.playerViews[playerView.PlayerType] = playerView;
            this.players[playerView.PlayerType] = new Player();
        }

        public void HandleHorizontalInput(PlayerType playerType, float input)
        {
            var player = this.players[playerType];
            this.playerViews[playerType].Move(input * player.MoveSpeed);
        }

        public void HandleJumpInput(PlayerType playerType)
        {
            var player = this.players[playerType];
            this.playerViews[playerType].Jump(player.JumpModifier);
        }
    }
}