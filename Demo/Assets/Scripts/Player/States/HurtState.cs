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
            _playerContext.Controls.Game.Disable();
        }

        public override void Exit()
        {
            _hurtTimer.ResetTimer();
            _playerContext.Controls.Game.Enable();
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
                if (_playerContext.Controls.Game.Attack.WasPressedThisFrame())
                {
                    _playerContext.PlayerController.ChangeState(PlayerStates.Attack);
                }
                if (_playerContext.Controls.Game.SpinAttack.WasPressedThisFrame())
                {
                    _playerContext.PlayerController.ChangeState(PlayerStates.SpinAttack);
                }
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