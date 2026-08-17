namespace UnityDemo.Player
{
    public class MovementState : PlayerStateBase
    {
        public MovementState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        protected virtual void Move(float speed)
        {
        }
    }
}