using System;
using UnityEngine;

namespace UI
{
    public class PlayerHealthCounterUI : MonoBehaviour
    {
        [SerializeField] private Player.PlayerHealth _playerHealth;
        private TMPro.TMP_Text _healthText;



        private void Awake()
        {
            _healthText = GetComponent<TMPro.TMP_Text>();
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
            _healthText.text = $"Здоровье: {health}";
        }
    }
}
