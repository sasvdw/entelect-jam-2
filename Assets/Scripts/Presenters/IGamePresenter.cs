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

        void StepPlayerPhysics(PlayerType playerType);
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

            if(!player.CanMove)
            {
                return;
            }

            if(Math.Abs(input) < 0.0001f && (!player.WasKicked || player.WasMoving))
            {
                player.StopedMoving();
                return;
            }

            player.StartedMoving(input);
        }

        public void HandleJumpInput(PlayerType playerType)
        {
            var player = this.players[playerType];

            if(!player.CanJump)
            {
                return;
            }

            player.Jump();
        }

        public void PlayerLanded(PlayerType playerType)
        {
            this.players[playerType].Landed();
        }

        public void PlayerInRangeForKick(PlayerType playerTypeKicking, PlayerType playerTypeToKick)
        {
            var playerKicking = this.players[playerTypeKicking];
            var playerToKick = this.players[playerTypeToKick];
            playerKicking.SetPlayerInRangeToKick(playerToKick);
        }

        public void HandleKickInput(PlayerType playerType)
        {
            this.players[playerType].Kick();
        }

        public void PlayerOutRangeForKick(PlayerType playerKicking)
        {
            this.players[playerKicking].SetPlayerOutRangeForKicking();
        }

        public void StepPlayerPhysics(PlayerType playerType)
        {
            var player = this.players[playerType];
            var playerView = this.playerViews[playerType];

            if(player.HadJumped)
            {
                playerView.Jump(player.JumpForce);
                player.Jumped();
            }

            if (player.WasKicked)
            {
                playerView.Kick(player.KickForce);
                player.Kicked();
                return;
            }

            if (player.WasMoving)
            {
                playerView.Move(player.MoveSpeedMagnitude);
            }

            if(!player.WasMoving && !player.WasKicked)
            {
                playerView.Stop();
            }
        }
    }
}