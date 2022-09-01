using System.Collections;
using System.Collections.Generic;
using TankU.SaveData;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TankU.PlayerHistory
{
    public class MatchHistoryController : MonoBehaviour
    {
        [SerializeField] private Transform _contentContainer;
        [SerializeField] private GameObject _tableListTemplate;
        [SerializeField] private Button _exitButton;

        private void Awake()
        {
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void Start()
        {
            LoadDataFromSaveData();
        }

        private void LoadDataFromSaveData()
        {
            print(PlayerPrefs.GetString("Player Data"));
            List<PlayerProgress> _playerData = DataController.Instance.GetallPlayerProgress();
            if (DataController.Instance.TotalPlayer <= 0) return;
            for (int i = 0; i < _playerData[0].TotalPlay; i++)
            {
                var _instantiated = Instantiate(_tableListTemplate, _contentContainer);
                var _texts = _instantiated.GetComponentsInChildren<TMP_Text>();
                for (int j = 0; j < DataController.Instance.TotalPlayer; j++)
                {
                    _texts[j].text = _playerData[j].GetHistoryByIndex(i).ToString();
                }
            }

        }


        private void OnExitButtonClicked()
        {
            gameObject.SetActive(false);
        }
    }
}

