using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private int _totalPlayer = 2;
        [SerializeField] private int _maximumPlayerHP;

        public event Action<int> OnGameOver;
        public event Action<int, int> OnPlayerHealthChange;
        public event Action<List<int>> OnPlayerHealthInit;
        private List<int> _playerHPs = new();

        private void Start()
        {
            for(int i = 0; i < _totalPlayer; i++)
            {
                _playerHPs.Add(_maximumPlayerHP);
            }
            OnPlayerHealthInit?.Invoke(_playerHPs);
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
                OnGameOver?.Invoke(_playerIndex + 1);
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
            List<int> _possibleTie = new List<int>();
            var _mostHP = _playerHPs.Max();
            int _winner = -1;
            do
            {
                _winner = _playerHPs.FindIndex(a => a == _mostHP);
                _possibleTie.Add(_winner);
                _playerHPs.RemoveAt(_winner);
            } while (_winner != -1);

            if (_possibleTie.Count > 1)
            {
                OnGameOver?.Invoke(0);
            }
            else if (_possibleTie.Count == 1)
            {
                OnGameOver?.Invoke(_possibleTie[0] + 1);
            }
        }
    }
}

