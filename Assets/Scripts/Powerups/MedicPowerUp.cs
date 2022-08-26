using System;
using TankU.PlayerObject;
using UnityEngine;

namespace TankU.PowerUP
{
    public class MedicPowerUp : MonoBehaviour, IPickable
    {
        public GameObject medicpowerFX;

        public event Action<int, PowerUpController.Type> OnPlayerPicked;

        private void OnTriggerEnter(Collider other)
        {
            IBuffable _buffInterface = other.gameObject.GetComponent<IBuffable>();

            if (_buffInterface == null) return;

            OnPlayerPicked?.Invoke(_buffInterface.Index, PowerUpController.Type.Medic);
            gameObject.SetActive(false);
        }
    }
}

