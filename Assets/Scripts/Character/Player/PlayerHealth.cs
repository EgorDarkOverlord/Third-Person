using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : CharacterHealth
    {
        public override float Health
        {
            get => base.Health;
            set 
            { 
                base.Health = value;

                if (base.Health < 0)
                {
                    base.Health = 0;
                }

                OnHealthChanged?.Invoke(base.Health, MaxHealth);
            }
        }

        public event Action<float, float> OnHealthChanged;
    }
}
