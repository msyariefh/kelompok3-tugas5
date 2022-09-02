using System.Collections.Generic;
using UnityEngine;

namespace TankU.SaveData
{
    [System.Serializable]
    public class PlayerProgress
    {
        [SerializeField] private int _id;
        [SerializeField] private List<MatchResult> _matchHistory = new();
        [SerializeField] private int _totalWin;
        [SerializeField] private int _totalPlay;
        [SerializeField] private int _totalOpenedSkin;
        [SerializeField] private int _totalExp;
        [SerializeField] private int _level;
        public int Id => _id;
        public int TotalWin => _totalWin;
        public int TotalPlay => _totalPlay;
        public int TotalOpenedSkin => _totalOpenedSkin;
        public int TotalExp => _totalExp;
        public int Level => _level;

        public enum MatchResult
        {
            WIN,
            LOSE,
            TIE
        }

        public void SetId(int _i)
        {
            _id = _i;
        }

        public MatchResult GetHistoryByIndex(int _index)
        {
            return _matchHistory[_index];
        }

        public void AddMatchResult(MatchResult _result)
        {
            _matchHistory.Add(_result);
            _totalExp += CalculateTotalExp(_result);
            if (_result == MatchResult.WIN)
            {
                _totalWin++;
            }
            _totalPlay++;
            ConvertExpToLevel();
        }

        public void AddSkin()
        {
            _totalOpenedSkin++;
        }

        private void ConvertMatchToExp()
        {
            int _temp = 0;
            for (int i = 0; i < _matchHistory.Count; i++)
            {
                _temp += CalculateTotalExp(_matchHistory[i]);
            }
            _totalExp = _temp;
        }

        private void ConvertExpToLevel()
        {
            _level = Mathf.FloorToInt(_totalExp / 500f);
        }

        private int CalculateTotalExp(MatchResult _result)
        {
            return _result switch
            {
                MatchResult.WIN => 100,
                MatchResult.LOSE => 50,
                MatchResult.TIE => 50,
                _ => 0,
            };
        }

        public void UpdateFromOldData()
        {
            ConvertMatchToExp();
            ConvertExpToLevel();
        }

    }
}
