using UnityEngine;

namespace UnityDemo.Player
{
    public class SpinAttackState : PlayerStateBase
    {
        private Timer _spinAttackTimer;

        public SpinAttackState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
            _spinAttackTimer = new Timer(playerContext.SpinAttackTime);
        }

        public override void Enter()
        {
            _playerContext.Animator.SetTrigger("SpinAttack");
        }

        public override void Exit()
        {
            _spinAttackTimer.ResetTimer();
        }

        public override void UpdateState()
        {
            _spinAttackTimer.UpdateTimer(Time.deltaTime);
            CheckTransition();
            _playerContext.Weapon.TryDealDamage(PlayerStates.SpinAttack);
        }

        public override void CheckTransition()
        {
            if (_spinAttackTimer.IsTimerFinished())
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