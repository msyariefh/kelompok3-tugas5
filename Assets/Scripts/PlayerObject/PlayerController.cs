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
        private float _moveSpeed;
        [SerializeField]
        private float _rotateSpeed;
        [SerializeField]
        private InputController _inputController;
        [SerializeField]
        private Transform _tankHead;
        [SerializeField]
        private Transform _tankBody;
        [SerializeField] private Transform _shootPos;

        //private CharacterController _controller;
        private Rigidbody _rigidbody;
        public event Action<bool, Transform, int> OnPlayerShoot;
        public event Action<Transform> OnBombPlanted;
        public event Action<int> OnPowerUpEnded;
        public event Action<int> OnPowerUpStarted;

        private int _powerUpTimeLeft = 0;
        private bool _isWaiting = false;
        private bool _isGamePaused = false;
        private Vector3 _currentLooking = Vector3.zero;

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
            //controller = GetComponent<CharacterController>();
            _rigidbody = GetComponent<Rigidbody>();

        }
        private void Update()
        {
            if (_inputController.PlayerShootInput()) OnPlayerShoot?.Invoke(_powerUpTimeLeft > 0, _shootPos, _playerIndex);
            if (_inputController.BombPlantInput()) OnBombPlanted?.Invoke(transform);
            if (_powerUpTimeLeft < 1) return;
            else
            {

            }
            if (_isWaiting) return;
            StartCoroutine(PowerUpTimer());
        }

        private void FixedUpdate()
        {
            Vector3 _moveInput = _inputController.ProcessMoveInput();
            //_controller.Move(_moveSpeed * Time.fixedDeltaTime * _moveInput);
            _rigidbody.velocity = _moveInput * _moveSpeed * 10 * Time.fixedDeltaTime;
            _tankHead.Rotate(_inputController.ProcessRotateInput() * _rotateSpeed);
            if (_currentLooking != _moveInput)
            {
                //_tankBody.rotation = Quaternion.LookRotation(_moveInput);
                transform.rotation = Quaternion.LookRotation(_moveInput);
            }
            
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
            print($"Player {_playerIndex} has {_powerUpTimeLeft}s");
            if (_powerUpTimeLeft == 0) OnPowerUpEnded?.Invoke(_playerIndex);
            _isWaiting = false;
        }
    }
}

