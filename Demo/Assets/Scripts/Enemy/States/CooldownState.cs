using UnityEngine;

namespace UnityDemo.Enemy
{
    public class CooldownState : EnemyStateBase
    {
        public CooldownState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter Cooldown state");
        }

        public override void Exit()
        {
            Debug.Log("Exit Cooldown state");
        }

        public override void UpdateState()
        {
        }

        public override void CheckTransition()
        {
        }
    }
}