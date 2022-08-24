using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.PowerUP
{
    public class MedicPowerUp : MonoBehaviour
    {
        public GameObject medicpowerFX;
        public event Action OnMedicPowerUp;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnMedicPowerUp?.Invoke();
            }
        }
    }
}

