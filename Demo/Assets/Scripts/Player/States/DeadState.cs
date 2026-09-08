namespace UnityDemo.Player
{
    public class DeadState : PlayerStateBase
    {
        public DeadState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        public override void Enter()
        {
        }

        public override void UpdateState()
        {
        }
    }
}