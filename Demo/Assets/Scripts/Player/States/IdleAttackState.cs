using UnityEngine;

namespace UnityDemo.Player
{
    public class IdleAttackState : MovementState
    {
        public IdleAttackState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        public override void Enter()
        {
            // Play attack animation
            Debug.Log("Enter IdleAttack State");
        }

        public override void Exit()
        {
            Debug.Log("Exit IdleAttack State");
        }

        public override void UpdateState()
        {
            CheckTransition();
        }

        public override void CheckTransition()
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