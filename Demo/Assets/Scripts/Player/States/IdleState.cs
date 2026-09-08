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
        }

        public override void Exit()
        {
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
                    _playerContext.PlayerController.ChangeState(PlayerStates.Run);
                    return;
                }
                _playerContext.PlayerController.ChangeState(PlayerStates.Walk);
            }
        }
    }
}
