using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityDemo.Enemy
{
    public class CooldownState : EnemyStateBase
    {
        private Timer _cooldownTimer;

        public CooldownState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
            _cooldownTimer = new Timer(enemyContext.CooldownTime);
        }

        public override void Enter()
        {
            _enemyContext.Animator.SetTrigger("Cooldown");
        }

        public override void Exit()
        {
            _cooldownTimer.ResetTimer();
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            _cooldownTimer.UpdateTimer(Time.deltaTime);
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_cooldownTimer.IsTimerFinished())
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
                    _enemyContext.EnemyController.ChangeState(EnemyStates.Idle);
                }
            }
        }
    }
}