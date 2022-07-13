using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float MaxHealth;

    [SerializeField]
    private float _health;
    private bool _isAlive = true;
    public virtual float Health
    {
        get => _health;
        set 
        {
            _health = value;

            if(value > MaxHealth)
            {
                _health = MaxHealth;
            }

            if(value <= 0 && _isAlive)
            {
                _isAlive = false;
                OnDied?.Invoke();
            }
        }
    }

    public event Action OnDied;
}