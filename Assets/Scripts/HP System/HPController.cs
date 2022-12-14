using System;
using System.Collections.Generic;
using System.Linq;
using TankU.Bomb;
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
        [SerializeField] private BomController _bomController;
        [SerializeField] private int _totalPlayer = 2;
        [SerializeField] private int _maximumPlayerHP;

        public event Action<int> OnGameOver;
        public event Action<int, int> OnPlayerHealthChange;
        public event Action<List<int>> OnPlayerHealthInit;
        private List<int> _playerHPs = new();
        private bool _isGameOver = false;

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
            _projectileController.OnProjectileHitPlayer += OnPlayerHit;
            _bomController.OnBombHitPlayer += OnPlayerHit;
        }
        private void OnDisable()
        {
            _timerController.OnTimesUp -= OnTimesUp;
            _powerUpController.OnMedicPowerUp -= OnMedicPowerUp;
            _projectileController.OnProjectileHitPlayer -= OnPlayerHit;
            _bomController.OnBombHitPlayer -= OnPlayerHit;
        }

        private void OnPlayerHit(int _playerIndex, int _damage)
        {
            if (_isGameOver) return;
            if(_playerHPs[_playerIndex] - _damage < 1)
            {
                _playerHPs[_playerIndex] = 0;
                _isGameOver = true;
                OnPlayerHealthChange?.Invoke(_playerIndex, _playerHPs[_playerIndex]);

                InvokeGameOverCondition();
                _timerController.GetComponent<IPausable>().OnGameOver(0);
                return;
            }
            _playerHPs[_playerIndex] -= _damage;
            OnPlayerHealthChange?.Invoke(_playerIndex, _playerHPs[_playerIndex]);
        }

        private void OnMedicPowerUp(int _playerIndex, int _health)
        {
            if (_playerHPs[_playerIndex] + _health > _maximumPlayerHP)
            {
                _playerHPs[_playerIndex] = _maximumPlayerHP;
            }
            else
            {
                _playerHPs[_playerIndex] += _health;
            }
            
            OnPlayerHealthChange?.Invoke(_playerIndex, _playerHPs[_playerIndex]);
        }

        private void OnTimesUp()
        {
            InvokeGameOverCondition();
        }
        private void InvokeGameOverCondition()
        {
            List<int> _possibleTie = new();
            var _mostHP = _playerHPs.Max();

            int _winner = -1;
            do
            {
                _winner = _playerHPs.IndexOf(_mostHP);
                if (_winner == -1) break;
                _possibleTie.Add(_winner);
                _playerHPs.RemoveAt(_winner);

            } while (_winner != -1);

            if (_possibleTie.Count > 1)
            {
                OnGameOver?.Invoke(-1);
            }
            else if (_possibleTie.Count == 1)
            {
                OnGameOver?.Invoke(_possibleTie[0]);
            }
        }
    }
}

