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
            _playerContext.Animator.SetBool("Walk", true);
        }

        public override void Exit()
        {
            _playerContext.Animator.SetBool("Walk", false);
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
                _playerContext.PlayerController.ChangeState(PlayerStates.Sprint);
            }
            if (_playerContext.Controls.Game.Attack.WasPressedThisFrame())
            {
                _playerContext.PlayerController.ChangeState(PlayerStates.MovingAttack);
            }
        }
    }
}