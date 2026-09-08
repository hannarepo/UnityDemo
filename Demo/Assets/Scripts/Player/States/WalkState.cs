using UnityEngine;

namespace UnityDemo.Player
{
    public class WalkState : MovementState
    {
        public WalkState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        public override void Enter()
        {
            // Change animation state
        }

        public override void Exit()
        {
            // Change animation state
        }

        public override void UpdateState()
        {
            Move(_playerContext.WalkSpeed);
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_playerContext.Controls.Game.Move.ReadValue<Vector2>() == Vector2.zero)
            {
                _playerContext.PlayerController.ChangeState(PlayerStates.Idle);
            }
            if (_playerContext.Controls.Game.Sprint.WasPressedThisFrame())
            {
                _playerContext.PlayerController.ChangeState(PlayerStates.Run);
            }
            if (_playerContext.Controls.Game.Attack.WasPressedThisFrame())
            {
                _playerContext.PlayerController.ChangeState(PlayerStates.MovingAttack);
            }
        }
    }
}