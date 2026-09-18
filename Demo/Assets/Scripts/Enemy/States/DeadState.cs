using System;

namespace UnityDemo.Enemy
{
    public class DeadState : EnemyStateBase
    {
        public static event Action OnEnemyDeath;

        public DeadState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }

        public override void Enter()
        {
            _enemyContext.Animator.SetBool("Dead", true);
            if (OnEnemyDeath != null) OnEnemyDeath.Invoke();
        }
    }
}