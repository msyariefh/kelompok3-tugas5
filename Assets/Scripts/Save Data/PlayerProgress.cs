using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.SaveData
{
    [System.Serializable]
    public class PlayerProgress
    {
        public int Id { get; private set; }
        private List<MatchResult> _matchHistory = new List<MatchResult>();
        private int _totalWin;

        public enum MatchResult
        {
            WIN,
            LOSE,
            TIE
        }

        public int GetPlayerTotalWin()
        {
            return _totalWin;
        }

        public List<MatchResult> GetPlayerHistory()
        {
            return _matchHistory;
        }

        public void SetNewHistory(MatchResult _lastResult)
        {
            _matchHistory.Add(_lastResult);
        }

        public void SetId(int _id)
        {
            Id = _id;
        }
    }
}
