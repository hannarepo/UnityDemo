using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityDemo.Player
{
    public class SprintState : MovementState
    {
        public SprintState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        public override void Enter()
        {
            _playerContext.Animator.SetBool("Sprint", true);
            _playerContext.Controls.Game.Sprint.canceled += contextCancelled => OnSprintRelease(contextCancelled);
        }

        public override void Exit()
        {
            _playerContext.Animator.SetBool("Sprint", false);
            _playerContext.Controls.Game.Sprint.canceled -= contextCancelled => OnSprintRelease(contextCancelled);
        }

        public override void UpdateState()
        {
            Move(_playerContext.RunSpeed);
            CheckTransition();
        }

        public override void CheckTransition()
        {
            
            if (_playerContext.Controls.Game.Move.ReadValue<Vector2>() == Vector2.zero)
            {
                _playerContext.PlayerController.ChangeState(PlayerStates.Idle);
            }
        }

        private void OnSprintRelease(InputAction.CallbackContext context)
        {
            if (context.control.device is Keyboard)
            {
                if (_playerContext.Controls.Game.Move.ReadValue<Vector2>() != Vector2.zero)
                {
                    _playerContext.PlayerController.ChangeState(PlayerStates.Walk);
                }
            }
        }
    }
}