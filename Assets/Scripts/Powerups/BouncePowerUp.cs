using System;
using TankU.Audio;
using TankU.PlayerObject;
using UnityEngine;

namespace TankU.PowerUP
{
    public class BouncePowerUp : MonoBehaviour, IPickable
    {
        //public GameObject bouncepowerFX;
        public event Action<int, PowerUpController.Type> OnPlayerPicked;

        void Start()
        {
            transform.Rotate(new Vector3(35, 0, 0));
        }

        private void OnTriggerEnter(Collider other)
        {
            IBuffable _buffInterface = other.gameObject.GetComponentInParent<IBuffable>();

            if (_buffInterface == null) return;

            OnPlayerPicked?.Invoke(_buffInterface.Index, PowerUpController.Type.Bounce);
            AudioManager.Instance.PlaySFX("ShotChargingSFX");
            gameObject.SetActive(false);
        }

   
    }
}

