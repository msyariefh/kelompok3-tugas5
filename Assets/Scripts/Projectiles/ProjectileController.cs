using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Projectile
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private int _damageGivenPerProjectile;

        public event Action<int, int> OnPlayerHit;
        private List<GameObject> _projectilePool = new List<GameObject>();


        private void OnEnable()
        {
            
        }
        private void OnDisable()
        {
            
        }

        private void OnHitPlayer(int _index)
        {
            OnPlayerHit?.Invoke(_index, _damageGivenPerProjectile);
        }

        private void OnPlayerShoot(Vector3 _shootPosition, Quaternion _playerRotation)
        {
            GameObject _projectileReady = GetObjectFromPool(_projectilePool);
            if(_projectileReady == null)
            {
                _projectileReady = Instantiate(_projectilePrefab, _shootPosition, _playerRotation);
                _projectileReady.SetActive(true);
                IHitable _hitableInterface = _projectileReady.GetComponent<IHitable>();
                _hitableInterface.OnHitPlayer += OnHitPlayer;
                _projectilePool.Add(_projectileReady);
            }
            else
            {
                _projectileReady.transform.position = _shootPosition;
                _projectileReady.transform.rotation = _playerRotation;
                _projectileReady.SetActive(true);
            }
        }
        private GameObject GetObjectFromPool(List<GameObject> _pool)
        {
            return _pool.Find(go => !go.activeInHierarchy);
        }
    }

}
