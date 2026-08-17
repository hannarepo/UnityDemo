namespace UnityDemo.Player
{
    public class PlayerStateBase : State<PlayerStates>
    {
        protected PlayerContext _playerContext;
        
        public PlayerStateBase(PlayerStates state, PlayerStates transitions, PlayerContext playerContext) : base(state, transitions)
        {
            _playerContext = playerContext;
        }

        public override void CheckTransition() { }
        public override void Enter() { }
        public override void Exit() { }
        public override void FixedUpdateState() { }
        public override void LateUpdateState() { }
        public override void UpdateState() { }
    }
}
