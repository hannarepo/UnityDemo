using System.Collections;
using UnityEngine;

namespace UnityDemo.Enemy
{
    public class AggroState : EnemyStateBase
    {
        private Timer _aggroTimer;

        public AggroState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
            _aggroTimer = new Timer(_enemyContext.AggroTime);
        }

        public override void Enter()
        {
            _enemyContext.Animator.SetTrigger("Aggro");
        }

        public override void Exit()
        {
            _aggroTimer.ResetTimer();
        }


        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            _aggroTimer.UpdateTimer(Time.deltaTime);
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_aggroTimer.IsTimerFinished())
            {
                if (_playerInAggroRange)
                {
                    if (_canSeePlayer && _distanceToPlayer <= _enemyContext.AttackDistance)
                    {
                        _enemyContext.EnemyController.ChangeState(EnemyStates.Attack);
                    }
                    else
                    {
                        _enemyContext.EnemyController.ChangeState(EnemyStates.Charge);
                    }
                }
                else
                {
                    _enemyContext.EnemyController.ChangeState(EnemyStates.Patrol);
                }
            }
        }
    }
}