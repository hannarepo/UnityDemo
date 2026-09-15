using UnityEngine;

namespace UnityDemo.Player
{
    public class AttackState : PlayerStateBase
    {
        private Timer _attackTimer;

        public AttackState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
            _attackTimer = new Timer(playerContext.AttackTime);
        }

        public override void Enter()
        {
            _playerContext.Weapon.TryDealDamage(PlayerStates.Attack);
            _playerContext.Animator.SetTrigger("Attack");
        }

        public override void Exit()
        {
            _attackTimer.ResetTimer();
        }

        public override void UpdateState()
        {
            _attackTimer.UpdateTimer(Time.deltaTime);
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_attackTimer.IsTimerFinished())
            {
                if (_playerContext.Controls.Game.Move.ReadValue<Vector2>() != Vector2.zero)
                {
                    _playerContext.PlayerController.ChangeState(PlayerStates.Walk);
                }
                else
                {
                    _playerContext.PlayerController.BackToPreviousState();
                }
            }
        }
    }
}