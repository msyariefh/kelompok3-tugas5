using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TankU.PlayerInput;

namespace TankU.Pause
{

    public class Pause : MonoBehaviour
    {
        //[SerializeField] private PlayerInput _playerInput;
        
        private void OnEnable()
        {
          //  _playerInput.OnGamePause += OnGamePause;
        }
        private void OnDisable()
        {
          //  _playerInput.OnGamePause -= OnGamePause;
        }


        void OnGamePause(bool pause)
        {
            if (pause)
                transform.GetChild(0).gameObject.SetActive(true);

            else if (!pause)
                 transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}