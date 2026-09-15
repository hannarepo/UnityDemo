using UnityEngine;

namespace UnityDemo.Enemy
{
    [CreateAssetMenu(fileName = "New EnemyContext", menuName = "Enemy/EnemyContext")]
    public class EnemyContext : ScriptableObject
    {
        // Movement
		[field: SerializeField] public float WalkSpeed { get; set; }
		[field: SerializeField] public float TurnSpeed { get; set; }
		[field: SerializeField] public WaypointPath.Direction Direction { get; set; }
		[field: SerializeField] public float StopInterval { get; set; }
        [field: SerializeField] public float StopTime { get; set; }
		[field: SerializeField] public WaypointPath WaypointPath { get; set; }

        // Components
		public Rigidbody Rigidbody { get; set; }
		public Transform Transform { get; set; }
        public EnemyController EnemyController { get; set; }
        public Animator Animator { get; set; }

        // Player detection
        [field: SerializeField] public float VisionDistance { get; set; }
        [field: SerializeField] public float FieldOfVision { get; set; }
        [field: SerializeField] public float SphereCastRadius { get; set; }
        [field: SerializeField] public LayerMask SphereCastLayers { get; set; }

        // Combat
        [field: SerializeField] public float HurtTime { get; set; }
        [field: SerializeField] public float AggroTime { get; set; }
        [field: SerializeField] public float ChargeSpeed { get; set; }
        [field: SerializeField] public float AttackTime { get; set; }
        [field: SerializeField] public float AttackDistance { get; set; }
        [field: SerializeField] public float CooldownTime { get; set; }
        public Transform Player { get; set; }
        public EnemyWeapon Weapon { get; set; }
    }
}