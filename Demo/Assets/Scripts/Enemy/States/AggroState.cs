using UnityEngine;

namespace UnityDemo.Enemy
{
    public class AggroState : EnemyStateBase
    {
        public AggroState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter Aggro state");
        }

        public override void Exit()
        {
            Debug.Log("Exit Aggro state");
        }


        public override void UpdateState()
        {
        }

        public override void CheckTransition()
        {
        }
    }
}