using System;
using UnityEngine;

namespace TankU.PowerUP
{
    public class BouncePowerUp : MonoBehaviour, IPickable
    {
        public GameObject bouncepowerFX;
        public event Action<int, int> OnPlayerPicked;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player1"))
            {
                OnPlayerPicked?.Invoke(0, 0);
                gameObject.SetActive(false);
            }
            else if (other.CompareTag("Player2"))
            {
                OnPlayerPicked?.Invoke(1, 0);
                gameObject.SetActive(false);
            }
        }

   
    }
}

