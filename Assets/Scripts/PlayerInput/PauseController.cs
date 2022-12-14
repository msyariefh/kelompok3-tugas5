using System;
using System.Collections;
using System.Collections.Generic;
using TankU.Timer;
using UnityEngine;

namespace TankU.PlayerInput
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private KeyCode _pauseKey;
        [SerializeField] private CountdownController _countdownController;
        public event Action OnGamePause;
        public event Action OnGameResume;
        private bool _isGamePaused = true;

        private void Update()
        {
            if (Input.GetKeyDown(_pauseKey))
            {
                if (_isGamePaused)
                {
                    print("resume");
                    _isGamePaused = false;
                    OnGameResume?.Invoke();
                }
                else
                {
                    print("pause");
                    _isGamePaused = true;
                    OnGamePause?.Invoke();
                }
            }
        }

        private void OnEnable()
        {
            _countdownController.OnCountdownEnded += OnGameStarted;
        }

        private void OnDisable()
        {
            _countdownController.OnCountdownEnded -= OnGameStarted;
        }

        private void OnGameStarted()
        {
            _isGamePaused = false;
        }
    }
}

