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
        public override void FixedUpdateState() { }
        public override void LateUpdateState() { }
        public override void UpdateState() { }
    }
}