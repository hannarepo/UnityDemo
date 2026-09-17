using System.Collections;
using UnityEngine;

namespace UnityDemo.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private int _attackBaseDamage = 20;
        [SerializeField] private int _spinAttackBaseDamage = 10;
        [SerializeField] private LayerMask _damageLayers;
        [SerializeField] private float _attackCooldownDuration = 0.7f;
        [SerializeField] private float _spinAttackCooldownDuration = 1f;
        private bool _isOnCooldown = false;
        private bool _canDealDamage = false;
        private PlayerStates _currentState = PlayerStates.None;

        public int AttackBaseDamage
        {
            get { return _attackBaseDamage; }
            set { _attackBaseDamage = value; }
        }

        public int SpinAttackBaseDamage
        {
            get { return _spinAttackBaseDamage; }
            set { _spinAttackBaseDamage = value; }
        }

        #region Public Methods

        /// <summary>
        /// Try to deal damage. Will succeed if an damageable collider is found.
        /// </summary>
        /// <param name="currentState"> The current player state. </param>
        public void TryDealDamage(PlayerStates currentState)
        {
            if (_isOnCooldown) return;

            _canDealDamage = true;
            _currentState = currentState;
            StartCoroutine(Cooldown(currentState));
        }

        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider other)
        {
            if (!_canDealDamage) return;

            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth == null)
            {
                enemyHealth = other.GetComponentInParent<Health>();
            }

            if (enemyHealth != null)
            {
                if (_currentState == PlayerStates.Attack)
                {
                    enemyHealth.TakeDamage(_attackBaseDamage);
                }
                else if (_currentState == PlayerStates.SpinAttack)
                {
                    enemyHealth.TakeDamage(_spinAttackBaseDamage);
                }
            }
            _canDealDamage = false;
        }

        private IEnumerator Cooldown(PlayerStates attackState)
        {
            _isOnCooldown = true;
            float timeElapsed = 0f;
            float duration = 0f;

            if (attackState == PlayerStates.Attack) duration = _attackCooldownDuration;
            else if (attackState == PlayerStates.SpinAttack) duration = _spinAttackCooldownDuration;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            _isOnCooldown = false;
        }

        #endregion
    }
}
