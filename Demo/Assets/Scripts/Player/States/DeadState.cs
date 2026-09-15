namespace UnityDemo.Player
{
    public class DeadState : PlayerStateBase
    {
        public DeadState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        public override void Enter()
        {
            _playerContext.Controls.Game.Disable();
            _playerContext.Animator.SetBool("Dead", true);
        }
    }
}