using UnityEngine;

namespace UnityDemo.Player
{
    public class VictoryState : PlayerStateBase
    {
        private Timer _victoryTimer;

        public VictoryState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
            _victoryTimer = new Timer(_playerContext.VictoryTime);
        }

        public override void Enter()
        {
            _playerContext.Animator.SetTrigger("Victory");
        }

        public override void Exit()
        {
            _victoryTimer.ResetTimer();
        }

        public override void UpdateState()
        {
            _victoryTimer.UpdateTimer(Time.deltaTime);
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_victoryTimer.IsTimerFinished())
            {
                if (_playerContext.Controls.Game.Move.ReadValue<Vector2>() != Vector2.zero)
                {
                    _playerContext.PlayerController.ChangeState(PlayerStates.Idle);
                }
                else
                {
                    _playerContext.PlayerController.ChangeState(PlayerStates.Walk);
                }
            }
        }
    }
}