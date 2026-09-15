using UnityEngine;

namespace UnityDemo.Player
{
    [CreateAssetMenu(fileName = "New PlayerContext", menuName = "Player/PlayerContext")]
    public class PlayerContext : ScriptableObject
    {
        // Movement
        [field: SerializeField] public float WalkSpeed { get; set; }
        [field: SerializeField] public float RotationSpeed { get; set; }
        [field: SerializeField] public float RunSpeed {get; set; }

        // Components
        public PlayerControls Controls { get; set; }
        public Transform PlayerTransform { get; set; }
        public Transform CameraTransform { get; set; }
        public PlayerController PlayerController { get; set; }
        public Animator Animator { get; set; }

        // Combat
        [field: SerializeField] public float AttackTime { get; set; }
        [field: SerializeField] public float SpinAttackTime { get; set; }
        [field: SerializeField] public float HurtTime { get; set; }
        public PlayerWeapon Weapon { get; set; }
    }
}