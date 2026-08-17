using UnityEngine;

namespace UnityDemo.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyContext _enemyContext = null;
        private FiniteStateMachine<EnemyStates> _stateMachine;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            EnemyStates idleTransitions = 
                EnemyStates.Movement | EnemyStates.Aggro | EnemyStates.Attack | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates movementTransitions =
                EnemyStates.Idle | EnemyStates.Aggro | EnemyStates.Attack | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates aggroTransitions =
                EnemyStates.Attack | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates attackTransitions =
                EnemyStates.Cooldown | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates cooldownTransitions =
                EnemyStates.Idle | EnemyStates.Movement | EnemyStates.Aggro | EnemyStates.Attack | EnemyStates.Hurt | EnemyStates.Dead;
            EnemyStates hurtTransitions =
                EnemyStates.Idle | EnemyStates.Dead;
            EnemyStates deadTransitions =
                EnemyStates.None;

            EnemyStateBase[] enemyStates =
            {
                new IdleState(EnemyStates.Idle, idleTransitions, _enemyContext),
                new MovementState(EnemyStates.Movement, movementTransitions, _enemyContext),
                new AggroState(EnemyStates.Aggro, aggroTransitions, _enemyContext),
                new AttackState(EnemyStates.Attack, attackTransitions, _enemyContext),
                new CooldownState(EnemyStates.Cooldown, cooldownTransitions, _enemyContext),
                new HurtState(EnemyStates.Hurt, hurtTransitions, _enemyContext),
                new DeadState(EnemyStates.Dead, deadTransitions, _enemyContext)
            };
            _stateMachine = new FiniteStateMachine<EnemyStates>(enemyStates, EnemyStates.Idle);
        }

        private void Update()
        {
            _stateMachine.UpdateState();
        }
    }
}
