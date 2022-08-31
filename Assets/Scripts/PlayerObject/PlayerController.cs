using System;
using System.Collections;
using System.Collections.Generic;
using TankU.Audio;
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
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;
        private InputController _inputController;
        [SerializeField]
        private Transform _tankHead;
        [SerializeField]
        private Transform _tankBody;
        [SerializeField] private Transform _shootPos;
        [SerializeField] Renderer[] _rendererToBeChangedInStart;

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
        private bool _isGameStarted = false;

        private Sound _lastLoopingSoundPlayed;

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
            _rigidbody = GetComponent<Rigidbody>();
            _inputController = GetComponent<InputController>();
            

        }
        private void Update()
        {
            if (_isGameStarted != _inputController.IsGameStarted)
            {
                _isGameStarted = _inputController.IsGameStarted;
                ColorUtility.TryParseHtmlString($"#{PlayerPrefs.GetString($"Player{_playerIndex} Color")}", out Color _playerColor);
                SetPropertyColor(_playerColor);
            }
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

            if (_inputController.IsGameOver)
                AudioManager.Instance.StopAllSFX();
            // Sound when idle
            if (_isGameStarted && !_inputController.IsGameOver)
            {
                if (_moveInput == Vector3.zero)
                    LoopingAudioSfxProcess("EngineIdleSFX");
                else LoopingAudioSfxProcess("EngineDriveSFX");
            }
            

            _rigidbody.velocity = _moveSpeed * 10 * Time.fixedDeltaTime * _moveInput;
            _tankHead.Rotate(_inputController.ProcessRotateInput() * _rotateSpeed);
            if (_moveInput == Vector3.zero) return;
            if (_currentLooking != _moveInput && _currentLooking != -1 * _moveInput)
            {
                _tankBody.rotation = Quaternion.LookRotation(_moveInput);
                _currentLooking = _moveInput;
            }

        }

        private void LoopingAudioSfxProcess(string _sfxTobePlayed)
        {
            if (_lastLoopingSoundPlayed == null)
            {
                _lastLoopingSoundPlayed = AudioManager.Instance.PlayLoopingSFX(_sfxTobePlayed);
                return;
            }
            if (_lastLoopingSoundPlayed.Name != _sfxTobePlayed)
            {
                AudioManager.Instance.StopLoopingSFX(_lastLoopingSoundPlayed);
                _lastLoopingSoundPlayed = AudioManager.Instance.PlayLoopingSFX(_sfxTobePlayed);
                return;
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
            if (_powerUpTimeLeft == 0) OnPowerUpEnded?.Invoke(_playerIndex);
            _isWaiting = false;
        }

        private void SetPropertyColor(Color _color)
        {
            var _propBlock = new MaterialPropertyBlock();
            
            foreach (Renderer _r in _rendererToBeChangedInStart)
            {
                _r.GetPropertyBlock(_propBlock);
                _propBlock.SetColor("_Color", _color);
                _r.SetPropertyBlock(_propBlock, 0);
            }
        }
    }
}

