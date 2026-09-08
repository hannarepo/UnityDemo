using UnityEngine;

namespace UnityDemo.Player
{
    public class MovingAttackState : MovementState
    {
        public MovingAttackState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        public override void Enter()
        {
            // Play attack animation
            Debug.Log("Enter MovingAttack State");
        }

        public override void Exit()
        {
            Debug.Log("Exit MovingAttack State");
        }

        public override void UpdateState()
        {
            CheckTransition();
            Move(_playerContext.WalkSpeedAttacking);
        }

        public override void CheckTransition()
        {
            if (_playerContext.Controls.Game.Move.ReadValue<Vector2>() == Vector2.zero)
            {
                _playerContext.PlayerController.ChangeState(PlayerStates.Idle);
            }
            else
            {
                _playerContext.PlayerController.BackToPreviousState();
            }
        }
    }
}