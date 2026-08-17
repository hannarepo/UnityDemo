namespace UnityDemo.Enemy
{
    public class CooldownState : EnemyStateBase
    {
        public CooldownState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }
    }
}