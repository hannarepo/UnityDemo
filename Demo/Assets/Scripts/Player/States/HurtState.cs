using UnityEngine;

namespace UnityDemo.Player
{
    public class HurtState : PlayerStateBase
    {
        public HurtState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter Hurt State");
        }

        public override void Exit()
        {
            Debug.Log("Exit Hurt State");
        }

        public override void UpdateState()
        {
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_playerContext.Controls.Game.Move.ReadValue<Vector2>() == Vector2.zero)
            {
                _playerContext.PlayerController.ChangeState(PlayerStates.Idle);
            }
            else
            {
                if (_playerContext.Controls.Game.Sprint.WasPressedThisFrame())
                {
                    _playerContext.PlayerController.ChangeState(PlayerStates.Run);
                }
                _playerContext.PlayerController.ChangeState(PlayerStates.Walk);
            }
        }
    }
}