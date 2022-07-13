using System;
using UnityEngine;

namespace UI
{
    public class PlayerHealthBarUI : MonoBehaviour
    {
        [SerializeField] private Player.PlayerHealth _playerHealth;

        private UnityEngine.UI.Image _healthBar;

        [SerializeField] private Color _maxHealthColor;
        [SerializeField] private Color _minHealthColor;



        private void Awake()
        {
            _healthBar = GetComponent<UnityEngine.UI.Image>();

            _healthBar.color = _maxHealthColor;
        }

        private void OnEnable()
        {
            _playerHealth.OnHealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _playerHealth.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float health, float maxHealth)
        {
            _healthBar.rectTransform.localScale = new Vector3(health / maxHealth, 1, 1);

            _healthBar.color = Color.Lerp(_minHealthColor, _maxHealthColor, health / maxHealth);
        }
    }
}
