namespace UnityDemo.Enemy
{
    public class AggroState : EnemyStateBase
    {
        public AggroState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }
    }
}