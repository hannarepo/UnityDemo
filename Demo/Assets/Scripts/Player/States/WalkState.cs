namespace UnityDemo.Player
{
    public class WalkState : MovementState
    {
        public WalkState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        public override void UpdateState()
        {
            Move(_playerContext.WalkSpeed);
        }
    }
}