using UnityEngine;

namespace UnityDemo.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyContext _enemyContext = null;
        [SerializeField] private WaypointPath _path;
        private Health _health = null;
        private FiniteStateMachine<EnemyStates> _stateMachine = null;

        #region Unity Methods

        private void Awake()
        {
            _health = GetComponent<Health>();
            _enemyContext.EnemyController = this;
            _enemyContext.Transform = transform;
            _enemyContext.WaypointPath = _path;
            _enemyContext.Rigidbody = GetComponent<Rigidbody>();
            _enemyContext.Animator = GetComponent<Animator>();
            _enemyContext.Weapon = GetComponentInChildren<EnemyWeapon>();
            Initialize();
        }

        private void OnEnable()
        {
            _health.OnDamage += OnTakeDamage;
        }

        private void OnDisable()
        {
            _health.OnDamage -= OnTakeDamage;
        }

        private void Update()
        {
            _stateMachine.UpdateState();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdateState();
        }

        private void OnDrawGizmos()
		{
			Debug.DrawRay(transform.position, transform.forward * _enemyContext.VisionDistance, Color.red);
			Gizmos.DrawWireSphere(transform.position, _enemyContext.SphereCastRadius);
		}

        #endregion

        #region Public Methods

        public void ChangeState(EnemyStates state)
        {
            _stateMachine.ChangeState(state);
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            EnemyStates idleTransitions = 
                EnemyStates.Patrol | EnemyStates.Aggro | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates walkTransitions =
                EnemyStates.Idle | EnemyStates.Aggro | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates aggroTransitions =
                EnemyStates.Idle | EnemyStates.Patrol |EnemyStates.Charge | EnemyStates.Attack | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates chargeTransitions =
                EnemyStates.Idle | EnemyStates.Patrol | EnemyStates.Aggro | EnemyStates.Attack | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates attackTransitions =
                EnemyStates.Cooldown | EnemyStates.Dead;
            EnemyStates cooldownTransitions =
                EnemyStates.Idle | EnemyStates.Patrol | EnemyStates.Aggro | EnemyStates.Charge | EnemyStates.Attack | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates hurtTransitions =
                EnemyStates.Idle | EnemyStates.Aggro | EnemyStates.Charge | EnemyStates.Attack | EnemyStates.Dead;
            EnemyStates deadTransitions =
                EnemyStates.None;

            EnemyStateBase[] enemyStates =
            {
                new IdleState(EnemyStates.Idle, idleTransitions, _enemyContext),
                new PatrolState(EnemyStates.Patrol, walkTransitions, _enemyContext),
                new AggroState(EnemyStates.Aggro, aggroTransitions, _enemyContext),
                new ChargeState(EnemyStates.Charge, chargeTransitions, _enemyContext),
                new AttackState(EnemyStates.Attack, attackTransitions, _enemyContext),
                new CooldownState(EnemyStates.Cooldown, cooldownTransitions, _enemyContext),
                new HurtState(EnemyStates.Hurt, hurtTransitions, _enemyContext),
                new DeadState(EnemyStates.Dead, deadTransitions, _enemyContext)
            };
            _stateMachine = new FiniteStateMachine<EnemyStates>(enemyStates, EnemyStates.Idle);
        }

        private void OnTakeDamage(int currentHealth)
        {
            Debug.Log("Enemy took damage, current health: " + currentHealth);
            if (currentHealth == 0) ChangeState(EnemyStates.Dead);
            else if (_stateMachine.CurrentState.StateKey != EnemyStates.Attack) ChangeState(EnemyStates.Hurt);
        }

        #endregion
    }
}
