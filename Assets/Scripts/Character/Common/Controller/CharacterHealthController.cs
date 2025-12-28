using System;

namespace Character.Common.Controller
{
    [Serializable]
    public class CharacterHealthController
    {
        private BaseCharacter _character;
        private float _maxHealth;
        private float _currentHealth;

        public bool IsAlive => _currentHealth > 0;
        
        public Action<float, float> OnHealthChanged;
        public Action<BaseCharacter> OnDeadAction;
        public Action OnDeadCompleteAction;

        public void OnStart(BaseCharacter character)
        {
            _character = character;
            InitialSet();
        }

        public void InitialSet()
        {
            SetMaxHealth(_character.CharacterData.Health);
            SetHealth(_maxHealth);
        }
        
        public void TakeDamage(float damage)
        {
            SetHealth(_currentHealth - damage);
        }
        
        public void OnDeadCompleted()
        {
            OnDeadCompleteAction?.Invoke();
        }
        
        private void OnDead()
        {
            OnDeadAction?.Invoke(_character);
        }

        private void SetHealth(float health)
        {
            _currentHealth = health;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnDead();
            }
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        private void SetMaxHealth(float health)
        {
            _maxHealth = health;
        }
    }
}
