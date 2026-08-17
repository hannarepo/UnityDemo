namespace UnityDemo.Enemy
{
    public class AttackState : EnemyStateBase
    {
        public AttackState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }
    }
}