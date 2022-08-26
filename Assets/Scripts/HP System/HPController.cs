using System;
using System.Collections;
using System.Collections.Generic;
using TankU.PowerUP;
using TankU.Projectile;
using TankU.Timer;
using UnityEngine;

namespace TankU.HPSystem
{
    public class HPController: MonoBehaviour
    {
        [SerializeField] private TimerController _timerController;
        [SerializeField] private PowerUpController _powerUpController;
        [SerializeField] private ProjectileController _projectileController;
        [SerializeField] private int _maximumPlayerHP;

        public event Action<int> OnGameOver;
        public event Action<int, int> OnPlayerHealthChange;
        private int[] _playerHPs;

        private void Start()
        {
            _playerHPs = new int[2];
            _playerHPs[0] = _maximumPlayerHP;
            _playerHPs[1] = _maximumPlayerHP;
        }

        private void OnEnable()
        {
            _timerController.OnTimesUp += OnTimesUp;
            _powerUpController.OnMedicPowerUp += OnMedicPowerUp;
            _projectileController.OnPlayerHit += OnPlayerHit;
        }
        private void OnDisable()
        {
            _timerController.OnTimesUp -= OnTimesUp;
            _powerUpController.OnMedicPowerUp -= OnMedicPowerUp;
            _projectileController.OnPlayerHit -= OnPlayerHit;
        }

        private void OnPlayerHit(int _playerIndex, int _damage)
        {
            if(_playerHPs[_playerIndex] - _damage <= 0)
            {
                _playerHPs[_playerIndex] = 0;
                OnPlayerHealthChange?.Invoke(_playerIndex, _playerHPs[_playerIndex]);
                OnGameOver?.Invoke(_playerIndex == 0 ? 1 : 0);
                return;
            }
            _playerHPs[_playerIndex] -= _damage;
            OnPlayerHealthChange?.Invoke(_playerIndex, _playerHPs[_playerIndex]);
        }

        private void OnMedicPowerUp(int _playerIndex, int _health)
        {
            _playerHPs[_playerIndex] += _health;
            OnPlayerHealthChange?.Invoke(_playerIndex, _playerHPs[_playerIndex]);
        }

        private void OnTimesUp()
        {
            OnGameOver?.Invoke(_playerHPs[0] > _playerHPs[1] ? 1 : 
                _playerHPs[0] < _playerHPs[1] ? 2 : 3);
        }
    }
}

