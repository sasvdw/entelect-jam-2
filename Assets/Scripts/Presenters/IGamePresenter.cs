using System;
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

        void PlayerLanded(PlayerType playerType);
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
        private readonly IDictionary<PlayerType, Player> players;

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
            var playerView = this.playerViews[playerType];

            if(Math.Abs(input) < 0.0001f)
            {
                playerView.Stop();
            }

            playerView.Move(input * player.MoveSpeed);
        }

        public void HandleJumpInput(PlayerType playerType)
        {
            var player = this.players[playerType];

            if(!player.CanJump)
            {
                return;
            }

            this.playerViews[playerType].Jump(player.JumpModifier);
            player.Jumped();
        }

        public void PlayerLanded(PlayerType playerType)
        {
            this.players[playerType].Landed();
        }
    }
}