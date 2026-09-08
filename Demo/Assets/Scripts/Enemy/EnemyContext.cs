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
    }
}