using UnityEngine;

namespace UnityDemo.Enemy
{
    public class MovementState : EnemyStateBase
    {
        protected WaypointPath _path = null;
        protected bool _isStopped = false;
        protected Vector3 _movementDirection = Vector3.zero;

        public MovementState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
        }

        public void Move(Vector3 movementDirection, float speed)
        {
            Vector3 movement = movementDirection.normalized * speed * Time.deltaTime;
            Vector3 position = _enemyContext.Rigidbody.position;
            position += movement;
            _enemyContext.Rigidbody.MovePosition(position);

            movementDirection = Vector3.zero;
        }
    }
}