using NUnit.Framework;
using UnityEngine;

namespace UnityDemo.Player
{
    public class HurtState : PlayerStateBase
    {
        private Timer _hurtTimer;

        public HurtState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
            _hurtTimer = new Timer(_playerContext.HurtTime);
        }

        public override void Enter()
        {
            _playerContext.Animator.SetTrigger("TakeDamage");
        }

        public override void Exit()
        {
            _hurtTimer.ResetTimer();
        }

        public override void UpdateState()
        {
            _hurtTimer.UpdateTimer(Time.deltaTime);
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_hurtTimer.IsTimerFinished())
            {
                if (_playerContext.Controls.Game.Move.ReadValue<Vector2>() == Vector2.zero)
                {
                    _playerContext.PlayerController.ChangeState(PlayerStates.Idle);
                }
                else
                {
                    if (_playerContext.Controls.Game.Sprint.WasPressedThisFrame())
                    {
                        _playerContext.PlayerController.ChangeState(PlayerStates.Sprint);
                    }
                    _playerContext.PlayerController.ChangeState(PlayerStates.Walk);
                }
            }
        }
    }
}