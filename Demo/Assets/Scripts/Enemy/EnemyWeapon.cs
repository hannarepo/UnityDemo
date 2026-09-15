using UnityEngine;

namespace UnityDemo.Enemy
{
	public class EnemyWeapon : MonoBehaviour
	{
		[SerializeField] private int _damage;

		public bool CanDealDamage { get; set; }

		private void OnTriggerEnter(Collider other)
		{
			// Check if the object is a player and if the weapon can deal damage.
			// If the object is a player, decrease its health and invoke knockback event.
			if (CanDealDamage && other.gameObject.layer == LayerMask.NameToLayer("Player")
				 && other.gameObject.TryGetComponent<Health>(out Health health))
			{
				health.TakeDamage(_damage);
			}
		}
	}
}