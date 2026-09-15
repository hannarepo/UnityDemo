using System.Collections;
using UnityEngine;

namespace UnityDemo.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerContext _playerContext = null;
        [SerializeField] private Transform _camera = null;
        [SerializeField] private float _attackTime;
        [SerializeField] private float _spinAttackTime;
        [SerializeField] private PlayerWeapon _weapon = null;
        private PlayerControls _controls = null;
        private PushdownAutomaton<PlayerStates> _pushdownAutomaton = null;
        private Health _health = null;
        private Animator _animator = null;

        #region Unity Methods

        private void Awake()
        {
            _controls = new PlayerControls();
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();

            _playerContext.Controls = _controls;
            _playerContext.PlayerTransform = transform;
            _playerContext.CameraTransform = _camera;
            _playerContext.PlayerController = this;
            _playerContext.Animator = _animator;
            _playerContext.AttackTime = _attackTime;
            _playerContext.SpinAttackTime = _spinAttackTime;
            _playerContext.Weapon = _weapon;

            Initialize();
        }

        private void OnEnable()
        {
            _controls.Enable();
            _health.OnDamage += OnTakeDamage;
        }

        private void OnDisable()
        {
            _controls.Disable();
            _health.OnDamage -= OnTakeDamage;
        }

        private void Update()
        {
            _pushdownAutomaton.UpdateState();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialize player states, state transitions and pushdown automaton.
        /// </summary>
        private void Initialize()
        {
            PlayerStates idleTransitions = 
                PlayerStates.Walk | PlayerStates.Sprint | PlayerStates.Attack | PlayerStates.SpinAttack | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates walkTransitions =
                PlayerStates.Idle | PlayerStates.Sprint | PlayerStates.Attack | PlayerStates.SpinAttack | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates sprintTransitions =
                PlayerStates.Idle | PlayerStates.Walk | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates attackTransitions =
                PlayerStates.Idle | PlayerStates.Walk | PlayerStates.SpinAttack | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates spinAttackTransitions =
                PlayerStates.Walk | PlayerStates.Sprint | PlayerStates.Idle | PlayerStates.Attack | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates hurtTransitions =
                PlayerStates.Idle | PlayerStates.Walk | PlayerStates.Sprint | PlayerStates.Attack | PlayerStates.SpinAttack | PlayerStates.Dead;
            PlayerStates deadTransitions =
                PlayerStates.None;

            PlayerStateBase[] playerStates =
            {
                new IdleState(PlayerStates.Idle, idleTransitions, _playerContext),
                new AttackState(PlayerStates.Attack, attackTransitions, _playerContext),
                new WalkState(PlayerStates.Walk, walkTransitions, _playerContext),
                new SprintState(PlayerStates.Sprint, sprintTransitions, _playerContext),
                new SpinAttackState(PlayerStates.SpinAttack, spinAttackTransitions, _playerContext),
                new HurtState(PlayerStates.Hurt, hurtTransitions, _playerContext),
                new DeadState(PlayerStates.Dead, deadTransitions, _playerContext)
            };
            _pushdownAutomaton = new PushdownAutomaton<PlayerStates>(playerStates, PlayerStates.Idle);
        }

        private void OnTakeDamage(int currentHealth)
        {
            Debug.Log("Player took damage, current health: " + currentHealth);
            if (currentHealth == 0) ChangeState(PlayerStates.Dead);
            else ChangeState(PlayerStates.Hurt);
        }

        #endregion

        #region Public Methods

        public void ChangeState(PlayerStates newState)
        {
            _pushdownAutomaton.Push(newState);
        }

        public void BackToPreviousState()
        {
            _pushdownAutomaton.Pop();
        }

        #endregion
    }
}
