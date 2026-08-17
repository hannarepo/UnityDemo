using UnityEngine;

namespace UnityDemo.Player
{
    [CreateAssetMenu(fileName = "New PlayerContext", menuName = "Player/PlayerContext")]
    public class PlayerContext : ScriptableObject
    {
        [field: SerializeField] public float WalkSpeed = 5.0f;
        [field: SerializeField] public float RunSpeed = 10.0f;
        [field: SerializeField] public float WalkSpeedAttacking = 3.0f;
    }
}