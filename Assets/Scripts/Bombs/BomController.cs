using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Bomb
{
    public class BomController : MonoBehaviour
    {
        [SerializeField] private GameObject _bombPrefab;

        private List<GameObject> _bombPool = new List<GameObject>();

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void OnBombPlanted(Vector3 _plantedPosition)
        {
            GameObject _readyBomb = GetObjectFromPool(_bombPool);
            if (_readyBomb == null)
            {
                _readyBomb = Instantiate(_bombPrefab, _plantedPosition, Quaternion.identity);
                _readyBomb.SetActive(true);
                _bombPool.Add(_readyBomb);
            }
            else
            {
                _readyBomb.transform.position = _plantedPosition;
                _readyBomb.SetActive(true);
            }
        }

        private GameObject GetObjectFromPool(List<GameObject> _pool)
        {
            return _pool.Find(go => !go.activeInHierarchy);
        }
    }
}

