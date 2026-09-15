using UnityEngine;

namespace UnityDemo.Enemy
{
    public class ChargeState : MovementState
    {
        public ChargeState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }

        public override void Enter()
        {
            _enemyContext.Animator.SetBool("Charge", true);
        }

        public override void Exit()
        {
            _enemyContext.Animator.SetBool("Charge", false);
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();

            Vector3 targetDirection = new Vector3(_enemyContext.Player.position.x, _enemyContext.Transform.position.y, _enemyContext.Player.position.z);
			_movementDirection = Vector3.Normalize(targetDirection - _enemyContext.Transform.position);
			_enemyContext.Transform.forward = _movementDirection;
            Move(_movementDirection, _enemyContext.ChargeSpeed);

            CheckTransition();
        }

        public override void CheckTransition()
        {
            if (_distanceToPlayer <= _enemyContext.AttackDistance)
            {
                _enemyContext.EnemyController.ChangeState(EnemyStates.Attack);
            }
            else if (!_playerInAggroRange)
            {
                _enemyContext.EnemyController.ChangeState(EnemyStates.Patrol);
            }
        }
    }
}