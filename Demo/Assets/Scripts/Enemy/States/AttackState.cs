using UnityEngine;

namespace UnityDemo.Enemy
{
    public class AttackState : EnemyStateBase
    {
        public AttackState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter Attack state");
        }

        public override void UpdateState()
        {
            Debug.Log("Exit Attack state");
        }

        public override void CheckTransition()
        {
        }
    }
}