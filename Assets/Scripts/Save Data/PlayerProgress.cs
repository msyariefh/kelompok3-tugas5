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
        public int Id => _id;
        public int TotalWin => _totalWin;
        public int TotalPlay => _totalPlay;

        public int TotalOpenedSkin => _totalOpenedSkin;

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
            if (_result == MatchResult.WIN)
            {
                _totalWin++;
            }
            _totalPlay++;
        }

        public void AddSkin()
        {
            _totalOpenedSkin++;
        }

    }
}
