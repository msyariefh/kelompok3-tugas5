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
        //private int[] _totalWins;
        private int[] _levels;
        private int[] _totalOpenedSkins;

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
            //_totalWins = new int[_playerNum];
            _totalOpenedSkins = new int[_playerNum];
            _levels = new int[_playerNum];
            for (int i = 0; i < _indexSelected.Length; i++)
            {
                _indexSelected[i] = 0;
                //_totalWins[i] = DataController.Instance.GetPlayerProgress(i).TotalWin;
                _levels[i] = DataController.Instance.GetPlayerProgress(i).Level;
                _totalOpenedSkins[i] = DataController.Instance.GetPlayerProgress(i).TotalOpenedSkin;
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

            if (_totalOpenedSkins[_id] > 0)
            {
                for(int i = 0; i < _totalOpenedSkins[_id]; i++)
                {
                    _availableColors[_id].Add(_premium[i].color);
                }
            }

            if (_totalOpenedSkins[_id] >= _premium.Length) return;

            //for (int i = 0; i < _milestone.Length; i++)
            //{
            //    if (_totalWins[_id] >= _milestone[i])
            //    {
            //        _availableColors[_id].Add(_premium[i].color);
            //        DataController.Instance.GetPlayerProgress(_id).AddSkin();
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            for (int i = _totalOpenedSkins[_id]; i <= _premium.Length; i++)
            {
                if (_levels[_id] >= i * 2)
                {
                    _availableColors[_id].Add(_premium[i].color);
                    DataController.Instance.GetPlayerProgress(_id).AddSkin();
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


