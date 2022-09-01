using System.Collections.Generic;
using UnityEngine;

namespace TankU.SaveData
{
    [System.Serializable]
    public class PlayerData
    {
        [SerializeField] List<PlayerProgress> _playerData = new();

        public void AddPlayerData(PlayerProgress _playerProgress, int _index)
        {
            _playerProgress.SetId(_index);
            _playerData.Add(_playerProgress);
           
        }
        public List<PlayerProgress> GetPlayerData()
        {
            return _playerData;
        }
    }

}
