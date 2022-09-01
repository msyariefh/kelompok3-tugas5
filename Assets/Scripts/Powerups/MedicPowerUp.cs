using System;
using TankU.Audio;
using TankU.PlayerObject;
using UnityEngine;

namespace TankU.PowerUP
{
    public class MedicPowerUp : MonoBehaviour, IPickable
    {
        //public GameObject medicpowerFX;

        public event Action<int, PowerUpController.Type> OnPlayerPicked;

        private void OnTriggerEnter(Collider other)
        {
            IBuffable _buffInterface = other.gameObject.GetComponentInParent<IBuffable>();

            if (_buffInterface == null) return;

            OnPlayerPicked?.Invoke(_buffInterface.Index, PowerUpController.Type.Medic);
            AudioManager.Instance.PlaySFX("ShotChargingSFX");
            gameObject.SetActive(false);
        }
    }
}

