using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.PowerUP
{
    public class MedicPowerUp : MonoBehaviour, IPickable
    {
        public GameObject medicpowerFX;

        public event Action<int, int> OnPlayerPicked;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player1"))
            {
                OnPlayerPicked?.Invoke(0, 1);
            }
            else if (other.CompareTag("Player2"))
            {
                OnPlayerPicked?.Invoke(1, 1);
            }
        }
    }
}

