using UnityEngine;

namespace UnityDemo.Enemy
{
    public class WalkState : MovementState
    {
        public WalkState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }

        public override void Enter()
        {
            base.Enter();
            if (_path.Type == WaypointPath.PathType.DelayLoop || _path.Type == WaypointPath.PathType.DelayPingPong)
            {
                _isStopped = false;
            }
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_canSeePlayer)
            {
                _enemyContext.EnemyController.ChangeState(EnemyStates.Aggro);
            }
            else if (_isStopped)
            {
                _enemyContext.EnemyController.ChangeState(EnemyStates.Idle);
            }
        }
    }
}