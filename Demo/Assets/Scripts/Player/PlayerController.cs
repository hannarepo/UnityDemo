using UnityEngine;

namespace UnityDemo.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerContext _playerContext = null;
        private PlayerControls _controls;
        private PushdownAutomaton<PlayerStates> _pushdownAutomaton;

        private void Awake()
        {
            _controls = new PlayerControls();
            Initialize();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        /// <summary>
        /// Initialize player states, state transitions and pushdown automaton.
        /// </summary>
        private void Initialize()
        {
            PlayerStates idleTransitions = 
                PlayerStates.Walk | PlayerStates.IdleAttack | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates walkTransitions =
                PlayerStates.Idle | PlayerStates.Run | PlayerStates.MovingAttack | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates runTransitions =
                PlayerStates.Idle | PlayerStates.Walk | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates idleAttackTransitions =
                PlayerStates.Idle | PlayerStates.Walk | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates movingAttackTransitions =
                PlayerStates.Walk | PlayerStates.Run | PlayerStates.Idle | PlayerStates.Hurt | PlayerStates.Dead;
            PlayerStates hurtTransitions =
                PlayerStates.Idle | PlayerStates.Dead;
            PlayerStates deadTransitions =
                PlayerStates.None;

            PlayerStateBase[] playerStates =
            {
                new IdleState(PlayerStates.Idle, idleTransitions, _playerContext),
                new IdleAttackState(PlayerStates.IdleAttack, idleAttackTransitions, _playerContext),
                new WalkState(PlayerStates.Walk, walkTransitions, _playerContext),
                new RunState(PlayerStates.Run, runTransitions, _playerContext),
                new MovingAttackState(PlayerStates.MovingAttack, movingAttackTransitions, _playerContext),
                new HurtState(PlayerStates.Hurt, hurtTransitions, _playerContext),
                new DeadState(PlayerStates.Dead, deadTransitions, _playerContext)
            };
            _pushdownAutomaton = new PushdownAutomaton<PlayerStates>(playerStates, PlayerStates.Idle);
        }

        private void Update()
        {
            _pushdownAutomaton.UpdateState();
        }
    }
}
