namespace UnityDemo.Player
{
    public class IdleAttackState : MovementState
    {
        public IdleAttackState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }
    }
}