using System;
using UnityEngine;

namespace UnityDemo
{
    /// <summary>
    /// Basic health class with max health and an invulnerability time after taking damage.
    /// </summary>
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private float _invulnerabilityTime = 1.0f;
        private int _currentHealth = 0;
        private Timer _invulnerabilityTimer = null;

        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;
        public bool IsInvulnerable { get; set; }

        public event Action<int> OnDamage = null;
        public event Action<int> OnHeal = null;

        #region Unity Methods

        private void Start()
        {
            _currentHealth = _maxHealth;
            _invulnerabilityTimer = new Timer(_invulnerabilityTime);
        }

        private void Update()
        {
            if (IsInvulnerable)
            {
                _invulnerabilityTimer.UpdateTimer(Time.deltaTime);
            }
            if (_invulnerabilityTimer.IsTimerFinished())
            {
                IsInvulnerable = false;
                _invulnerabilityTimer.ResetTimer();
            }
        }

        #endregion

        #region Health Methods

        /// <summary>
        /// Decreases health by given amount and invokes HealthChanged event.
        /// </summary>
        /// <param name="amount"> The amount to decrease health. </param>
        public void TakeDamage(int amount)
        {
            if (IsInvulnerable) return;

            amount = Mathf.Abs(amount);

            if (_currentHealth > amount)
            {
                _currentHealth -= amount;
                IsInvulnerable = true;
            }
            else if (_currentHealth <= amount)
            {
                _currentHealth = 0;
            }

            if (OnDamage != null)
            {
                OnDamage.Invoke(_currentHealth);
            }
        }

        /// <summary>
        /// Increases health by given amount and invokes HealthChanged event.
        /// Health increment is capped to max health.
        /// </summary>
        /// <param name="amount"> The amount to increase health. </param>
        public void Heal(int amount)
        {
            amount = Mathf.Abs(amount);
            _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
            if (OnHeal != null)
            {
                OnHeal.Invoke(_currentHealth);
            }
        }

        #endregion
    }
}