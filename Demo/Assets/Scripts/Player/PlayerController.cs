using System.Collections;
using UnityEngine;

namespace UnityDemo.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerContext _playerContext = null;
        [SerializeField] private Transform _camera = null;
        private PlayerControls _controls = null;
        private PushdownAutomaton<PlayerStates> _pushdownAutomaton = null;
        private Health _health = null;
        private Animator _animator;

        #region Unity Methods

        private void Awake()
        {
            _controls = new PlayerControls();
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();

            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                switch(clip.name)
                {
                    case "Attack":
                        _playerContext.AttackTime = clip.length;
                        break;
                }
            }

            _playerContext.Controls = _controls;
            _playerContext.PlayerTransform = transform;
            _playerContext.CameraTransform = _camera;
            _playerContext.PlayerController = this;
            _playerContext.Animator = _animator;

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
                PlayerStates.Walk | PlayerStates.IdleAttack | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates walkTransitions =
                PlayerStates.Idle | PlayerStates.Sprint | PlayerStates.MovingAttack | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates runTransitions =
                PlayerStates.Idle | PlayerStates.Walk | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates idleAttackTransitions =
                PlayerStates.Idle | PlayerStates.Walk | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates movingAttackTransitions =
                PlayerStates.Walk | PlayerStates.Sprint | PlayerStates.Idle | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates hurtTransitions =
                PlayerStates.Idle | PlayerStates.Dead;
            PlayerStates deadTransitions =
                PlayerStates.None;

            PlayerStateBase[] playerStates =
            {
                new IdleState(PlayerStates.Idle, idleTransitions, _playerContext),
                new IdleAttackState(PlayerStates.IdleAttack, idleAttackTransitions, _playerContext),
                new WalkState(PlayerStates.Walk, walkTransitions, _playerContext),
                new SprintState(PlayerStates.Sprint, runTransitions, _playerContext),
                new MovingAttackState(PlayerStates.MovingAttack, movingAttackTransitions, _playerContext),
                new HurtState(PlayerStates.Hurt, hurtTransitions, _playerContext),
                new DeadState(PlayerStates.Dead, deadTransitions, _playerContext)
            };
            _pushdownAutomaton = new PushdownAutomaton<PlayerStates>(playerStates, PlayerStates.Idle);
        }

        private void OnTakeDamage(int currentHealth)
        {
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
