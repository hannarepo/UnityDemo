using UnityEngine;

namespace UnityDemo.Enemy
{
    public class IdleState : MovementState
    {
        private Timer _stopTimer = null;

        public IdleState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
            _stopTimer = new Timer(_enemyContext.StopTime);
        }

        public override void Enter()
        {
            base.Enter();
            _isStopped = true;
        }

        public override void Exit()
        {
            _isStopped = false;
            _stopTimer.ResetTimer();
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            _stopTimer.UpdateTimer(Time.deltaTime);
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_stopTimer.IsTimerFinished())
            {
                _enemyContext.EnemyController.ChangeState(EnemyStates.Patrol);
            }
            if (_canSeePlayer)
            {
                _enemyContext.EnemyController.ChangeState(EnemyStates.Aggro);
            }
        }
    }
}