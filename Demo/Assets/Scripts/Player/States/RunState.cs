namespace UnityDemo.Player
{
    public class RunState : MovementState
    {
        public RunState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        public override void UpdateState()
        {
            Move(_playerContext.RunSpeed);
        }
    }
}