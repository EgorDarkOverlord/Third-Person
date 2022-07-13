using System;
using UnityEngine;

namespace UI
{
    public class PlayerScoreUI : MonoBehaviour
    {
        [SerializeField] private Player.PlayerController _player;
        private TMPro.TMP_Text _scoreText;

        private void Awake()
        {
            _scoreText = GetComponent<TMPro.TMP_Text>();
        }

        private void OnEnable()
        {
            _player.OnScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _player.OnScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            _scoreText.text = $"Очки: {score}";
        }
    }
}
