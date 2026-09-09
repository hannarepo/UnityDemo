using UnityEngine;

namespace UnityDemo.Player
{
    public class IdleAttackState : MovementState
    {
        private Timer _animationTimer;

        public IdleAttackState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
            _animationTimer = new Timer(playerContext.AttackTime);
        }

        public override void Enter()
        {
            _playerContext.Animator.SetTrigger("IdleAttack");
        }

        public override void UpdateState()
        {
            _animationTimer.UpdateTimer(Time.deltaTime);
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_animationTimer.IsTimerFinished())
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