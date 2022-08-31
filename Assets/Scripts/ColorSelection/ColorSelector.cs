using System.Collections;
using System.Collections.Generic;
using TankU.SaveData;
using UnityEngine;
using UnityEngine.UI;

namespace TankU.ColorSelection
{
    public class ColorSelector : MonoBehaviour
    {
        [SerializeField] private ScriptableColor _colorList;
        [SerializeField]
        private Button btnNext;
        [SerializeField] 
        GameObject _tutorialPanel;
        [SerializeField]
        int _playerNum;
        [SerializeField]
        private GameObject[] colorView;
        [SerializeField]
        private Button[] btnPicks;

        [SerializeField] private int[] _milestone;

        private List<Color>[] _availableColors;
        private int[] _totalWins;

        private int[] _indexSelected;

        private void Awake()
        {
            _availableColors = new List<Color>[_playerNum];

            btnNext.onClick.AddListener(OnSelection);
            for (int i = 0; i < btnPicks.Length; i++)
            {
                int x = i;
                btnPicks[i].onClick.AddListener(delegate { ChangeMaterial(x); });
            }
            _indexSelected = new int[_playerNum];
            _totalWins = new int[_playerNum];
            for (int i = 0; i < _indexSelected.Length; i++)
            {
                _indexSelected[i] = 0;
                _totalWins[i] = DataController.Instance.GetPlayerProgress(i).TotalWin;
                InitAvailableColors(i);
                colorView[i].GetComponent<Image>().color = _availableColors[i][0];
            }
        }
        private void InitAvailableColors(int _id)
        {
            ColorTankList[] _free = _colorList.GetFreeColors();
            ColorTankList[] _premium = _colorList.GetPremiumColors();

            _availableColors[_id] = new List<Color>();

            foreach(ColorTankList colorTankList in _free)
            {
                _availableColors[_id].Add(colorTankList.color);
            }

            for (int i = 0; i < _milestone.Length; i++)
            {
                print(_totalWins[_id]);
                print(_milestone[i]);
                if (_totalWins[_id] >= _milestone[i])
                {
                    _availableColors[_id].Add(_premium[i].color);
                }
                else
                {
                    break;
                }
            }
        }

        public void ChangeMaterial(int _index)
        {
            if(_indexSelected[_index] == _availableColors[_index].Count - 1)
            {
                _indexSelected[_index] = 0;
            }
            else
            {
                _indexSelected[_index]++;
            }
            colorView[_index].GetComponent<Image>().color = _availableColors[_index][_indexSelected[_index]];
        }

        private void OnSelection()
        {
            for(int i = 0; i < _playerNum; i++)
            {
                PlayerPrefs.SetString($"Player{i} Color", ColorUtility.ToHtmlStringRGBA(_availableColors[i][_indexSelected[i]]));
            }
            PlayerPrefs.Save();


            _tutorialPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}


