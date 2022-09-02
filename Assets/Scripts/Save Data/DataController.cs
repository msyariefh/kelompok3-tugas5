using System.Collections.Generic;
using UnityEngine;

namespace TankU.SaveData
{
    public class DataController : MonoBehaviour
    {
        public static DataController Instance { get; private set; }
        private PlayerData _playerData;
        public int TotalPlayer { get; set; } = 2;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }

            _playerData = new();
            LoadAllPlayerProgress();
            DontDestroyOnLoad(gameObject);
        }

        private void LoadAllPlayerProgress()
        {
            if (!PlayerPrefs.HasKey("Player Data"))
            {
                SaveAllPlayerProgress();
            }
            _playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("Player Data"));
            if (_playerData.GetPlayerData().Count == 0)
            {
                for (int i = 0; i < TotalPlayer; i++)
                {
                    _playerData.AddPlayerData(new PlayerProgress(), i);
                }
            }
            else
            {
                List<PlayerProgress> _list = _playerData.GetPlayerData();
                for(int i = 0; i < _list.Count; i++)
                {
                    if (IsPlayerDataOld(_list[i]))
                    {
                        _list[i].UpdateFromOldData();
                    }
                }
            }
            SaveAllPlayerProgress();
        }

        public PlayerProgress GetPlayerProgress(int _id)
        {
            return _playerData.GetPlayerData()[_id];
        }
        public void SetPlayerProgress(int _id, PlayerProgress.MatchResult _result)
        {
            GetPlayerProgress(_id).AddMatchResult(_result);
            SaveAllPlayerProgress();
        }
        public List<PlayerProgress> GetallPlayerProgress()
        {
            return _playerData.GetPlayerData();
        }

        public void SaveAllPlayerProgress()
        {
            PlayerPrefs.SetString("Player Data", JsonUtility.ToJson(_playerData));
            PlayerPrefs.Save();
        }

        private bool IsPlayerDataOld(PlayerProgress _playerProgress)
        {
            if (_playerProgress.TotalExp == 0 && _playerProgress.TotalPlay > 0) return true;
            return false;
        }
    }

}