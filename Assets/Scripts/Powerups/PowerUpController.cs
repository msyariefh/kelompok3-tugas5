using System;
using System.Collections;
using System.Collections.Generic;
using TankU.PlayerInput;
using TankU.Timer;
using UnityEngine;

namespace TankU.PowerUP
{
    public class PowerUpController : MonoBehaviour, IPausable
    {
        public enum Type { Bounce, Medic }

        [SerializeField] private CountdownController _countdownController;
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private int _maximumPowerUpInField;
        [SerializeField] private GameObject _medicPowerUpPrefab;
        [SerializeField] private GameObject _bouncePowerUpPrefab;
        [SerializeField] private float _spawnCooldown;
        [SerializeField] private float _zPositionToSpawn;
        [SerializeField] private int _bouncePowerUpTime;
        [SerializeField] private int _medicHealthAmount;
        public event Action<int, int> OnBouncePowerUp;
        public event Action<int, int> OnMedicPowerUp;

        private List<GameObject> _medicPowerUpPool;
        private List<GameObject> _bouncePowerUpPool;
        private bool _isGamePaused = false;
        private int _currentPowerUpInField = 0;

        private const float RADIUS_FROM_OBSTACLE = .25f;

        private void Start()
        {
            _medicPowerUpPool = new List<GameObject>();
            _bouncePowerUpPool = new List<GameObject>();
        }
        private void Update()
        {
            if (_currentPowerUpInField < _maximumPowerUpInField)
                StartCoroutine(PowerUpProduction());
        }

        private void OnEnable()
        {
            _countdownController.OnCountdownEnded += OnCountdownEnded;
            _pauseController.OnGamePause += OnGamePaused;
            _pauseController.OnGameResume += OnGameResumed;
        }

        private void OnDisable()
        {
            _countdownController.OnCountdownEnded -= OnCountdownEnded;
            _pauseController.OnGamePause -= OnGamePaused;
            _pauseController.OnGameResume -= OnGameResumed;
        }

        private void OnCountdownEnded()
        {

        }

        public void OnGameOver(int index)
        {
            _isGamePaused = true;
        }

        public void OnGamePaused()
        {
            _isGamePaused = true;
        }

        public void OnGameResumed()
        {
            _isGamePaused = false;
        }

        private void OnPlayerPickedPowerUp(int _index, Type _type)
        {
            switch (_type)
            {
                case Type.Bounce:
                    OnBouncePowerUp?.Invoke(_index, _bouncePowerUpTime);
                    break;
                case Type.Medic:
                    OnMedicPowerUp?.Invoke(_index, _medicHealthAmount);
                    break;
            }
            _currentPowerUpInField--;
        }


        IEnumerator PowerUpProduction()
        {
            _currentPowerUpInField++;
            float _xPos = 0;
            float _yPos = 0;
            do
            {
                _xPos = UnityEngine.Random.Range(-10f, 10f);
                _yPos = UnityEngine.Random.Range(-10f, 10f);
            } while (CheckSpawnPosition(_xPos, _yPos));

            yield return new WaitUntil(() => !_isGamePaused);
            yield return new WaitForSeconds(_spawnCooldown);

            int _powerUpType = UnityEngine.Random.Range(0, 2);
            GameObject _objectFromPool;
            IPickable _pickableInterface;
            switch (_powerUpType)
            {
                case 0:
                    _objectFromPool = GetObjectFromPool(_bouncePowerUpPool);
                    if (_objectFromPool == null)
                    {
                        _objectFromPool = Instantiate(_bouncePowerUpPrefab,
                            new Vector3(_xPos, _yPos, _zPositionToSpawn), Quaternion.identity);
                        _pickableInterface = _objectFromPool.GetComponent<IPickable>();
                        _pickableInterface.OnPlayerPicked += OnPlayerPickedPowerUp;
                        _objectFromPool.SetActive(true);
                        _bouncePowerUpPool.Add(_objectFromPool);
                    }
                    else
                    {
                        _objectFromPool.transform.position = new Vector3(_xPos, _yPos, _zPositionToSpawn);
                        _objectFromPool.SetActive(true);
                    }
                    break;
                case 1:
                    _objectFromPool = GetObjectFromPool(_medicPowerUpPool);
                    if (_objectFromPool == null)
                    {
                        _objectFromPool = Instantiate(_medicPowerUpPrefab,
                            new Vector3(_xPos, _yPos, _zPositionToSpawn), Quaternion.identity);
                        _pickableInterface = _objectFromPool.GetComponent<IPickable>();
                        _pickableInterface.OnPlayerPicked += OnPlayerPickedPowerUp;
                        _objectFromPool.SetActive(true);
                        _medicPowerUpPool.Add(_objectFromPool);
                    }
                    else
                    {
                        _objectFromPool.transform.position = new Vector3(_xPos, _yPos, _zPositionToSpawn);
                        _objectFromPool.SetActive(true);
                    }
                    break;
            }
        }

        private GameObject GetObjectFromPool(List<GameObject> _pool)
        {
            return _pool.Find(go => !go.activeInHierarchy);
        }

        private bool CheckSpawnPosition(float _x, float _y)
        {
            Collider[] colls = Physics.OverlapSphere(new Vector3(_x, _y, _zPositionToSpawn),
                RADIUS_FROM_OBSTACLE);
            if (colls.Length > 0) return true;
            else return false;

        }
    }
}

