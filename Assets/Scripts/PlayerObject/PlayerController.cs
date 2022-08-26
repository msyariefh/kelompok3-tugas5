using System;
using System.Collections;
using System.Collections.Generic;
using TankU.PlayerInput;
using TankU.PowerUP;
using TankU.Timer;
using UnityEngine;

namespace TankU.PlayerObject
{
    public class PlayerController : MonoBehaviour, IPausable, IDamagable, IBuffable
    {
        [SerializeField] int _playerIndex = 0;
        [SerializeField] PowerUpController _powerUpController;
        [SerializeField] PauseController _pauseController;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float rotateSpeed;
        [SerializeField]
        private InputController inputController;
        [SerializeField]
        private Transform tankHead;

        private CharacterController controller;
        public event Action<bool, Transform, int> OnPlayerShoot;
        public event Action<Transform> OnBombPlanted;
        public event Action<int> OnPowerUpEnded;
        public event Action<int> OnPowerUpStarted;

        private int _powerUpTimeLeft = 0;
        private bool _isWaiting = false;
        private bool _isGamePaused = false;

        public int Index => _playerIndex;

        private void OnEnable()
        {
            _powerUpController.OnBouncePowerUp += OnBouncePowerUp;
            _pauseController.OnGamePause += OnGamePaused;
            _pauseController.OnGameResume += OnGameResumed;
        }
        private void OnDisable()
        {
            _powerUpController.OnBouncePowerUp -= OnBouncePowerUp;
            _pauseController.OnGamePause -= OnGamePaused;
            _pauseController.OnGameResume -= OnGameResumed;
        }

        private void OnBouncePowerUp(int _index, int _time)
        {
            if (_powerUpTimeLeft == 0) OnPowerUpStarted?.Invoke(_index);
            if (_index == _playerIndex) _powerUpTimeLeft += _time;
        }

        private void Awake()
        {
            controller = GetComponent<CharacterController>();

        }
        private void Update()
        {
            if (inputController.PlayerShootInput()) OnPlayerShoot?.Invoke(_powerUpTimeLeft > 0, transform, _playerIndex);
            if (inputController.BombPlantInput()) OnBombPlanted?.Invoke(transform);
            if (_powerUpTimeLeft < 1) return;
            else
            {

            }
            if (_isWaiting) return;
            StartCoroutine(PowerUpTimer());
        }

        private void FixedUpdate()
        {
            controller.Move(moveSpeed * Time.fixedDeltaTime * inputController.ProcessMoveInput());
            tankHead.Rotate(inputController.ProcessRotateInput() * rotateSpeed);
            
        }

        public void OnGamePaused()
        {
            _isGamePaused = true;
        }

        public void OnGameResumed()
        {
            _isGamePaused = false;
        }

        public void OnGameOver(int index)
        {
            _isGamePaused = true;
        }

        private IEnumerator PowerUpTimer()
        {
            _isWaiting = true;
            yield return new WaitUntil(() => !_isGamePaused);
            yield return new WaitForSeconds(1f);
            _powerUpTimeLeft--;
            if (_powerUpTimeLeft == 0) OnPowerUpEnded?.Invoke(_playerIndex);
            _isWaiting = false;
        }
    }
}

