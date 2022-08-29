
using System.Collections;
using TankU.HPSystem;
using TankU.Timer;
using UnityEngine;

namespace TankU.PlayerInput
{
    public class InputController : MonoBehaviour, IPausable
    {
        [SerializeField] private InputScriptable inputScriptable;
        [SerializeField] private CountdownController countdownController;
        [SerializeField] private HPController hpController;
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private float _playerShootInterval;
        [SerializeField] private float _playerPlantBombInterval;

        private bool _isInShootCooldown = false;
        private bool _isInPlantBombCooldown = false;



        private bool _isGamePaused = true;

        public void OnGameOver(int index)
        {
            _isGamePaused = true;
        }

        private void OnGameStarted()
        {
            _isGamePaused = false;
        }

        private void OnEnable()
        {
            _pauseController.OnGamePause += OnGamePaused;
            _pauseController.OnGameResume += OnGameResumed;
            countdownController.OnCountdownEnded += OnGameStarted;
            hpController.OnGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            _pauseController.OnGamePause -= OnGamePaused;
            _pauseController.OnGameResume -= OnGameResumed;
            countdownController.OnCountdownEnded -= OnGameStarted;
            hpController.OnGameOver -= OnGameOver;
        }

        public Vector3 ProcessMoveInput()
        {
            if (_isGamePaused) return Vector3.zero;
            if (Input.GetKey(inputScriptable.InputKeys.moveLeft)) return Vector3.left;
            if (Input.GetKey(inputScriptable.InputKeys.moveRight)) return Vector3.right;
            if (Input.GetKey(inputScriptable.InputKeys.moveForward)) return Vector3.forward;
            if (Input.GetKey(inputScriptable.InputKeys.moveBackward)) return Vector3.back;
            return Vector3.zero;
        }

        public Vector3 ProcessRotateInput()
        {
            if (_isGamePaused) return Vector3.zero;
            if (Input.GetKey(inputScriptable.InputKeys.rotateLeft)) return new Vector3(0, -1, 0);
            if (Input.GetKey(inputScriptable.InputKeys.rotateRight)) return new Vector3(0, 1, 0);
            return Vector3.zero;
        }

        public bool PlayerShootInput()
        {
            if (_isGamePaused) return false;
            if (_isInShootCooldown) return false;
            if (Input.GetKeyDown(inputScriptable.InputKeys.shoot))
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
            if (Input.GetKeyDown(inputScriptable.InputKeys.plantBomb))
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

