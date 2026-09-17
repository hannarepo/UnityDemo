using UnityEngine;

namespace UnityDemo.Enemy
{
    public class DeadState : EnemyStateBase
    {
        public DeadState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }

        public override void Enter()
        {
            _enemyContext.Animator.SetBool("Dead", true);
        }
    }
}