
using System.Collections;
using TankU.HPSystem;
using TankU.Timer;
using UnityEngine;

namespace TankU.PlayerInput
{
    public class InputController : MonoBehaviour, IPausable
    {
        [SerializeField] private InputScriptable _inputScriptable;
        [SerializeField] private CountdownController _countdownController;
        [SerializeField] private HPController _hpController;
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private float _playerShootInterval;
        [SerializeField] private float _playerPlantBombInterval;
        [SerializeField] private Transform _playerHead;

        private bool _isInShootCooldown = false;
        private bool _isInPlantBombCooldown = false;



        private bool _isGamePaused = true;
        public bool IsGameStarted { get; private set; } = false;
        public bool IsGameOver { get; private set; } = false;

        public void OnGameOver(int index)
        {
            _isGamePaused = true;
            IsGameOver = true;
        }

        private void OnGameStarted()
        {
            _isGamePaused = false;
            IsGameStarted = true;
        }

        private void OnEnable()
        {
            _pauseController.OnGamePause += OnGamePaused;
            _pauseController.OnGameResume += OnGameResumed;
            _countdownController.OnCountdownEnded += OnGameStarted;
            _hpController.OnGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            _pauseController.OnGamePause -= OnGamePaused;
            _pauseController.OnGameResume -= OnGameResumed;
            _countdownController.OnCountdownEnded -= OnGameStarted;
            _hpController.OnGameOver -= OnGameOver;
        }

        public Vector3 ProcessMoveInput()
        {
            if (_isGamePaused) return Vector3.zero;
            if (Input.GetKey(_inputScriptable.InputKeys.moveLeft)) return -_playerHead.transform.right;
            if (Input.GetKey(_inputScriptable.InputKeys.moveRight)) return _playerHead.transform.right;
            if (Input.GetKey(_inputScriptable.InputKeys.moveForward)) return _playerHead.transform.forward;
            if (Input.GetKey(_inputScriptable.InputKeys.moveBackward)) return -_playerHead.transform.forward;
            return Vector3.zero;
        }

        public Vector3 ProcessRotateInput()
        {
            if (_isGamePaused) return Vector3.zero;
            if (Input.GetKey(_inputScriptable.InputKeys.rotateLeft)) return new Vector3(0, -1, 0);
            if (Input.GetKey(_inputScriptable.InputKeys.rotateRight)) return new Vector3(0, 1, 0);
            return Vector3.zero;
        }

        public bool PlayerShootInput()
        {
            if (_isGamePaused) return false;
            if (_isInShootCooldown) return false;
            if (Input.GetKeyDown(_inputScriptable.InputKeys.shoot))
            {
                _isInShootCooldown = true;
                StartCoroutine(CooldownShoot(_playerShootInterval));
                return true;
            }
            return false;
        }

        public bool BombPlantInput()
        {
            if (_isGamePaused) return false;
            if (_isInPlantBombCooldown) return false;
            if (Input.GetKeyDown(_inputScriptable.InputKeys.plantBomb))
            {
                _isInPlantBombCooldown = true;
                StartCoroutine(CooldownPlantBomb(_playerPlantBombInterval));
                return true;
            }
            return false;
        }

        public void OnGamePaused()
        {
            _isGamePaused = true;
        }

        public void OnGameResumed()
        {
            _isGamePaused = false;
        }

        private IEnumerator CooldownShoot(float _cooldown)
        {
            yield return new WaitUntil(() => !_isGamePaused);
            yield return new WaitForSeconds(_cooldown);
            _isInShootCooldown = false;
        }
        private IEnumerator CooldownPlantBomb(float _cooldown)
        {
            yield return new WaitUntil(() => !_isGamePaused);
            yield return new WaitForSeconds(_cooldown);
            _isInPlantBombCooldown = false;
        }
    }
}

