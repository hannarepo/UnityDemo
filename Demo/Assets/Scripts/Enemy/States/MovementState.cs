namespace UnityDemo.Enemy
{
    public class MovementState : EnemyStateBase
    {
        public MovementState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }

        private void Move(float speed)
        {
            
        }
    }
}