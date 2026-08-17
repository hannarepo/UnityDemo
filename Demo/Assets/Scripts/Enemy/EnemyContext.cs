using UnityEngine;

namespace UnityDemo.Enemy
{
    [CreateAssetMenu(fileName = "New EnemyContext", menuName = "Enemy/EnemyContext")]
    public class EnemyContext : ScriptableObject
    {
        [field: SerializeField] float WalkSpeed = 3.0f;
    }
}