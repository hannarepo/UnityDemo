using UnityEngine;

namespace UnityDemo.Enemy
{
    public class AttackState : EnemyStateBase
    {
        private Timer _attackTimer;

        public AttackState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
            _attackTimer = new Timer(enemyContext.AttackTime);
        }

        public override void Enter()
        {
            _enemyContext.Animator.SetTrigger("Attack");
            _enemyContext.Weapon.CanDealDamage = true;
        }

        public override void Exit()
        {
            _attackTimer.ResetTimer();
            _enemyContext.Weapon.CanDealDamage = false;
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            _attackTimer.UpdateTimer(Time.deltaTime);
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_attackTimer.IsTimerFinished())
            {
                _enemyContext.EnemyController.ChangeState(EnemyStates.Cooldown);
            }
        }
    }
}