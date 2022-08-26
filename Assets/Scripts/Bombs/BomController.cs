using System.Collections;
using System.Collections.Generic;
using TankU.PlayerInput;
using TankU.PlayerObject;
using UnityEngine;

namespace TankU.Bomb
{
    public class BomController : MonoBehaviour
    {
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private PlayerController[] _playerControllers;
        [SerializeField] private GameObject _bombPrefab;

        private List<GameObject> _bombPool = new List<GameObject>();

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

        private void OnBombPlanted(Transform _player)
        {
            GameObject _readyBomb = GetObjectFromPool(_bombPool);
            if (_readyBomb == null)
            {
                _readyBomb = Instantiate(_bombPrefab, _player.position, Quaternion.identity);
                _readyBomb.GetComponent<Bomb>().SetPauseController(_pauseController);
                _readyBomb.SetActive(true);
                _bombPool.Add(_readyBomb);
            }
            else
            {
                _readyBomb.transform.position = _player.position;
                _readyBomb.SetActive(true);
            }
        }

        private GameObject GetObjectFromPool(List<GameObject> _pool)
        {
            return _pool.Find(go => !go.activeInHierarchy);
        }
    }
}

