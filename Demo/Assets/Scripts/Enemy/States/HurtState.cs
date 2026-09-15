using UnityEngine;

namespace UnityDemo.Enemy
{
    public class HurtState : EnemyStateBase
    {
        private Timer _hurtTimer;

        public HurtState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
            _hurtTimer = new Timer(enemyContext.HurtTime);
        }

        public override void Enter()
        {
            _enemyContext.Animator.SetTrigger("TakeDamage");
        }

        public override void Exit()
        {
            _hurtTimer.ResetTimer();
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            _hurtTimer.UpdateTimer(Time.deltaTime);
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_hurtTimer.IsTimerFinished())
            {
                if (_canSeePlayer)
                {
                    if (_distanceToPlayer <= _enemyContext.AttackDistance)
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
                    _enemyContext.EnemyController.ChangeState(EnemyStates.Aggro);
                }
            }
        }
    }
}