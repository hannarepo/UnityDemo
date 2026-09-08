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
            Initialize();
        }

        private void OnEnable()
        {
            _health.OnDamage += OnTakeDamage;
        }

        private void OnDissable()
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
                EnemyStates.Walk | EnemyStates.Aggro | EnemyStates.Attack | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates walkTransitions =
                EnemyStates.Idle | EnemyStates.Aggro | EnemyStates.Attack | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates aggroTransitions =
                EnemyStates.Attack | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates attackTransitions =
                EnemyStates.Cooldown | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates cooldownTransitions =
                EnemyStates.Idle | EnemyStates.Walk | EnemyStates.Aggro | EnemyStates.Attack | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates hurtTransitions =
                EnemyStates.Idle | EnemyStates.Dead;
            EnemyStates deadTransitions =
                EnemyStates.None;

            EnemyStateBase[] enemyStates =
            {
                new IdleState(EnemyStates.Idle, idleTransitions, _enemyContext),
                new WalkState(EnemyStates.Walk, walkTransitions, _enemyContext),
                new AggroState(EnemyStates.Aggro, aggroTransitions, _enemyContext),
                new AttackState(EnemyStates.Attack, attackTransitions, _enemyContext),
                new CooldownState(EnemyStates.Cooldown, cooldownTransitions, _enemyContext),
                new HurtState(EnemyStates.Hurt, hurtTransitions, _enemyContext),
                new DeadState(EnemyStates.Dead, deadTransitions, _enemyContext)
            };
            _stateMachine = new FiniteStateMachine<EnemyStates>(enemyStates, EnemyStates.Idle);
        }

        private void OnTakeDamage(int currentHealth)
        {
            if (currentHealth == 0) ChangeState(EnemyStates.Dead);
            else ChangeState(EnemyStates.Hurt);
        }

        #endregion
    }
}
