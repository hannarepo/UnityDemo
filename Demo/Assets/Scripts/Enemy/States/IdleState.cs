namespace UnityDemo.Enemy
{
    public class IdleState : MovementState
    {
        public IdleState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }
    }
}