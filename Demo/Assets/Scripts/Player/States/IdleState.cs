using UnityEngine;

namespace UnityDemo.Player
{
    public class IdleState : PlayerStateBase
    {
        public IdleState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        public override void Enter()
        {
            _playerContext.Animator.SetBool("Idle", true);
        }

        public override void Exit()
        {
            _playerContext.Animator.SetBool("Idle", false);
        }

        public override void UpdateState()
        {
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_playerContext.Controls.Game.Move.ReadValue<Vector2>() != Vector2.zero)
            {
                if (_playerContext.Controls.Game.Sprint.WasPressedThisFrame())
                {
                    _playerContext.PlayerController.ChangeState(PlayerStates.Sprint);
                    return;
                }
                _playerContext.PlayerController.ChangeState(PlayerStates.Walk);
            }
            if (_playerContext.Controls.Game.Attack.WasPressedThisFrame())
            {
                _playerContext.PlayerController.ChangeState(PlayerStates.Attack);
            }
            if (_playerContext.Controls.Game.SpinAttack.WasPressedThisFrame())
            {
                _playerContext.PlayerController.ChangeState(PlayerStates.SpinAttack);
            }
        }
    }
}
