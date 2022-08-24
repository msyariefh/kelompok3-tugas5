using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Timer
{
    public class TimerController : MonoBehaviour, IPausable
    {
        [SerializeField] private CountdownController _countdown;
        [SerializeField] private int _gameLimitInSecond;
        private bool _isGamePaused = false;
        public event Action<int> OnTimerChange;
        public event Action OnTimesUp;


        private void OnEnable()
        {
            _countdown.OnCountdownEnded += OnCountdownEnded;
        }

        private void OnDisable()
        {
            _countdown.OnCountdownEnded -= OnCountdownEnded;
        }

        public void OnGameOver()
        {
            _isGamePaused = true;
        }

        public void OnGamePaused()
        {
            _isGamePaused = true;
        }

        public void OnGameResumed()
        {
            _isGamePaused = false;
        }

        private void OnCountdownEnded()
        {
            StartCoroutine(StartTimer());
        }

        IEnumerator StartTimer()
        {
            do
            {
                print(_gameLimitInSecond);
                OnTimerChange?.Invoke(_gameLimitInSecond);
                yield return new WaitUntil(() => !_isGamePaused);
                yield return new WaitForSeconds(1f);
                _gameLimitInSecond--;

            } while (_gameLimitInSecond > 0);
            OnTimesUp?.Invoke();
        }
    }
}

