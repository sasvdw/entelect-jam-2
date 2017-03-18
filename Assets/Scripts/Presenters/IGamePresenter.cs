using System;
using System.Collections.Generic;
using Assets.Scripts.Models;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Presenters
{
    public interface IGamePresenter
    {
        void AddPlayer(IPlayerView playerView);

        void HandleHorizontalInput(PlayerType playerType, float input);

        void HandleJumpInput(PlayerType playerType);

        void PlayerLanded(PlayerType playerType);

        void PlayerInRangeForKick(PlayerType playerKicking, PlayerType playerToKick);

        void HandleKickInput(PlayerType playerType);

        void PlayerOutRangeForKick(PlayerType playerKicking);
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

            if(Math.Abs(input) < 0.0001f && (!player.WasKicked || player.WasMoving))
            {
                playerView.Stop();
                player.StopedMoving();
                return;
            }

            playerView.Move(player.MoveSpeedMagnitude);
            player.StartedMoving(input);
        }

        public void HandleJumpInput(PlayerType playerType)
        {
            var player = this.players[playerType];

            if(!player.CanJump)
            {
                return;
            }

            this.playerViews[playerType].Jump(player.JumpMagnitude);
            player.Jumped();
        }

        public void PlayerLanded(PlayerType playerType)
        {
            this.players[playerType].Landed();
        }

        public void PlayerInRangeForKick(PlayerType playerKicking, PlayerType playerToKick)
        {
            var player = this.players[playerKicking];
            player.SetPlayerInRangeToKick(playerToKick);
        }

        public void HandleKickInput(PlayerType playerType)
        {
            var player = this.players[playerType];

            var kick = player.Kick();

            if(!this.playerViews.ContainsKey(kick))
            {
                return;
            }

            this.playerViews[kick].Kick(player.KickMagnitude);
        }

        public void PlayerOutRangeForKick(PlayerType playerKicking)
        {
            this.players[playerKicking].SetPlayerOutRangeForKicking();
        }
    }
}