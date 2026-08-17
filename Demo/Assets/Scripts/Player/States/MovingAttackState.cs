namespace UnityDemo.Player
{
    public class MovingAttackState : MovementState
    {
        public MovingAttackState(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions, playerContext)
        {
        }

        public override void UpdateState()
        {
            Move(_playerContext.WalkSpeedAttacking);
        }
    }
}