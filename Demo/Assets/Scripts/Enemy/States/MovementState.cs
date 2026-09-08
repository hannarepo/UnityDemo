using UnityEngine;

namespace UnityDemo.Enemy
{
    public class MovementState : EnemyStateBase
    {
        protected WaypointPath _path = null;
        protected bool _isStopped = false;
        private Waypoint _target = null;
        private Vector3 _movementDirection = Vector3.zero;
        private float _distanceTravelled = 0f;
        private bool _isMovingToStart = false;
        private Timer _stopTimer = null;

        public MovementState(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions, enemyContext)
        {
        }

        public override void Enter()
        {
            _stopTimer = new Timer(_enemyContext.StopInterval);
            if (_path == null)
            {
                SetWaypointPath(_enemyContext.WaypointPath);
            }
        }

        public override void Exit()
        {
        }

        public override void FixedUpdateState()
        {
            // If enemy ia moving to the first waypoint.
            if (_isMovingToStart)
            {
                Vector3 directionToTarget = _target.transform.position - _enemyContext.Transform.position;
                if (Vector3.Dot(_movementDirection, directionToTarget) <= 0)
                {
                    // Target reached
                    _target = _path.GetNextWaypoint(_target, _enemyContext.Direction);
                    _movementDirection = _target.transform.position - _enemyContext.Transform.position;
                    _isMovingToStart = false;
                }
                FaceTowardsNextWaypoint(_target.transform.position);
                Move(_movementDirection, _enemyContext.WalkSpeed);
            }
            else if (_path != null && !_isStopped)
            {
                MoveAlongSpline();
            }
            _stopTimer.UpdateTimer(Time.deltaTime);
        }

        #region Path related

        /// <summary>
        /// Set the waypoint path that is used for movement.
        /// </summary>
        /// <param name="path">The path to be used.</param>
        public void SetWaypointPath(WaypointPath path)
        {
            _path = path;
            if (_path == null)
            {
                Debug.LogWarning("No waypoint path found!");
            }
            else
            {
                _target = _path.GetNextWaypoint(previous: _target, _enemyContext.Direction);
                _movementDirection = _target.transform.position - _enemyContext.Transform.position;
                _isMovingToStart = true;
                _distanceTravelled = 0;
            }
        }

        /// <summary>
        /// Change direction of the path.
        /// </summary>
        private void ToggleDirection()
        {
            _enemyContext.Direction = _enemyContext.Direction == WaypointPath.Direction.Forward
                ? WaypointPath.Direction.Backward
                : WaypointPath.Direction.Forward;
        }

        #endregion

        #region  Movement

        public void Move(Vector3 movementDirection, float speed)
        {
            Vector3 movement = movementDirection.normalized * speed * Time.deltaTime;
            Vector3 position = _enemyContext.Rigidbody.position;
            position += movement;
            _enemyContext.Rigidbody.MovePosition(position);

            movementDirection = Vector3.zero;
        }

        /// <summary>
        /// Move along the spline of the path.
        /// </summary>
        private void MoveAlongSpline()
        {
            // Set the direction multiplier based on the current direction of the path.
            int directionMultiplier = _enemyContext.Direction == WaypointPath.Direction.Forward ? 1 : -1;
            // Set the distance travelled based on the speed and direction. Distance is the point on the spline.
            _distanceTravelled = _distanceTravelled + directionMultiplier * _enemyContext.WalkSpeed * Time.deltaTime;
            float splineLength = _path.GetSplineLength();

            // If the path is a loop, use modulus to wrap the distance travelled around the spline length.
            // If the path is a ping pong, reverse the direction when the end is reached.
            if (_path.Type == WaypointPath.PathType.DelayLoop)
            {
                _distanceTravelled %= splineLength;
            }
            else
            {
                if (_distanceTravelled > splineLength)
                {
                    _distanceTravelled = 2 * splineLength - _distanceTravelled;
                    ToggleDirection();
                }
                if (_distanceTravelled < 0)
                {
                    _distanceTravelled = -1 * _distanceTravelled;
                    ToggleDirection();
                }
            }
            Vector3 position = _path.GetPointOnSpline(_distanceTravelled);
            FaceTowardsNextWaypoint(position);

            // If the stop timer is finished, move towards the target position.
            // Otherwise, stop the enemy and reset the timer.
            if (!_stopTimer.IsTimerFinished())
            {
                Move(position - _enemyContext.Transform.position, _enemyContext.WalkSpeed);
            }
            else
            {
                _isStopped = true;
                _stopTimer.ResetTimer();
            }
        }

        /// <summary>
        /// Rotate the enemy towards the next waypoint.
        /// </summary>
        /// <param name="target">The next waypoint.</param>
        private void FaceTowardsNextWaypoint(Vector3 target)
        {
            // Calculate the direction to the target and normalize it.
            Vector3 lookDirection = new Vector3(target.x, _enemyContext.Transform.position.y, target.z)
                - _enemyContext.Transform.position;
            lookDirection.Normalize();

            // Calculate the rotation needed to look at the target and apply it to the enemy's transform.
            // Use Quaternion.Slerp to limit the rotation speed based on the turn speed.
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            Quaternion limitedRotation = Quaternion.Slerp(_enemyContext.Transform.rotation, targetRotation,
                _enemyContext.TurnSpeed * Time.deltaTime);
            _enemyContext.Transform.rotation = limitedRotation;
        }

        #endregion
    }
}