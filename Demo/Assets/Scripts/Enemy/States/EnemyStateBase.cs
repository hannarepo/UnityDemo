using UnityEngine;

namespace UnityDemo.Enemy
{
    public class EnemyStateBase : State<EnemyStates>
    {
        protected EnemyContext _enemyContext;
        protected bool _playerInAggroRange = false;
        protected bool _canSeePlayer = false;
        protected float _distanceToPlayer = 0f;
        protected float _dot = 0f;
        protected Collider[] _colliders = new Collider[1];

        public EnemyStateBase(EnemyStates state, EnemyStates transitions, EnemyContext enemyContext) : base(state, transitions)
        {
            _enemyContext = enemyContext;
        }

        public override void CheckTransition() { }
        public override void Enter() { }
        public override void Exit() { }
        public override void LateUpdateState() { }
        public override void UpdateState() { }

        public override void FixedUpdateState()
        {
            if (!_playerInAggroRange)
            {
                FindPlayerInRange();
            }
            else
            {
                LookForPlayer();
            }
        }

        #region Player detection

        /// <summary>
        /// Checks if the player is within the aggro range of the enemy.
        /// </summary>
        public void FindPlayerInRange()
        {
            _colliders = Physics.OverlapSphere(_enemyContext.Transform.position, _enemyContext.SphereCastRadius,
                 _enemyContext.SphereCastLayers);
            if (_colliders.Length > 0)
            {
                _playerInAggroRange = true;
                _enemyContext.Player = _colliders[0].gameObject.transform;
                LookForPlayer();
            }
            else
            {
                _playerInAggroRange = false;
                _canSeePlayer = false;
            }
        }

        /// <summary>
        /// Checks if the enemy can see the player based on the field of vision and distance.
        /// </summary>
        public void LookForPlayer()
        {
            if (_enemyContext.Player == null) return;

            Vector3 directionToPlayer = Vector3.Normalize(_enemyContext.Player.position - _enemyContext.Transform.position);
            _dot = Vector3.Dot(_enemyContext.Transform.forward, directionToPlayer);
            _distanceToPlayer = Vector3.Distance(_enemyContext.Player.position, _enemyContext.Transform.position);

            if (_distanceToPlayer <= _enemyContext.VisionDistance)
            {
                _canSeePlayer = _dot >= _enemyContext.FieldOfVision;
            }
            else
            {
                _canSeePlayer = false;
                _playerInAggroRange = false;
            }
        }
        #endregion
    }
}