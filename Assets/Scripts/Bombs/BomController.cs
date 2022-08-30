using System;
using System.Collections;
using System.Collections.Generic;
using TankU.PlayerInput;
using TankU.PlayerObject;
using TankU.Projectile;
using UnityEngine;

namespace TankU.Bomb
{
    public class BomController : MonoBehaviour
    {
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private PlayerController[] _playerControllers;
        [SerializeField] private GameObject _bombPrefab;
        [SerializeField] private int _damageGivenByBomb = 1;

        private readonly List<GameObject> _bombPool = new ();
        public event Action<int, int> OnBombHitPlayer;

        private void OnEnable()
        {
            foreach(PlayerController _controller in _playerControllers)
            {
                _controller.OnBombPlanted += OnBombPlanted;
            }
        }

        private void OnDisable()
        {
            foreach (PlayerController _controller in _playerControllers)
            {
                _controller.OnBombPlanted -= OnBombPlanted;
            }
        }

        private void OnHitPlayer(int _index)
        {
            OnBombHitPlayer?.Invoke(_index, _damageGivenByBomb);
        }

        private void OnBombPlanted(Transform _player)
        {
            GameObject _readyBomb = GetObjectFromPool(_bombPool);
            if (_readyBomb == null)
            {
                _readyBomb = Instantiate(_bombPrefab, _player.position, Quaternion.identity);
                IHitable _hitableInterface = _readyBomb.GetComponent<IHitable>();
                _hitableInterface.OnHitPlayer += OnHitPlayer;
                _readyBomb.GetComponent<Bomb>().SetPauseController(_pauseController);
                _bombPool.Add(_readyBomb);
            }

            _readyBomb.transform.position = _player.position;
            _readyBomb.SetActive(true);
        }

        private GameObject GetObjectFromPool(List<GameObject> _pool)
        {
            return _pool.Find(go => !go.activeInHierarchy);
        }
    }
}

