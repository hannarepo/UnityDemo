namespace UnityDemo.Enemy
{
    public class EnemyStateBase : State<EnemyStates>
    {
        protected EnemyContext _enemyContext;

        public EnemyStateBase(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions)
        {
            _enemyContext = enemyContext;
        }

        public override void CheckTransition() { }
        public override void Enter() { }
        public override void Exit() { }
        public override void FixedUpdateState() { }
        public override void LateUpdateState() { }
        public override void UpdateState() { }
    }
}