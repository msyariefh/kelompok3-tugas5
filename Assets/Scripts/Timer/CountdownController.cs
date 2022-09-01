using System.Collections;
using UnityEngine;
using TMPro;
using System;
using TankU.Tutorials;
using TankU.PlayerInput;

namespace TankU.Timer
{
    public class CountdownController : MonoBehaviour, IPausable
    {
        [SerializeField] private Tutorial _tutorialController;
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private int _countdownNumber = 3;
        [SerializeField] private TMP_Text _countdownText;
        private bool _isGamePaused = false;
        public event Action OnCountdownEnded;

        private void OnEnable()
        {
            _tutorialController.OnPlayerReady += OnPlayerReady;
            _pauseController.OnGamePause += OnGamePaused;
            _pauseController.OnGameResume += OnGameResumed;
        }
        private void OnDisable()
        {
            _tutorialController.OnPlayerReady -= OnPlayerReady;
            _pauseController.OnGamePause -= OnGamePaused;
            _pauseController.OnGameResume -= OnGameResumed;
        }

        public void OnPlayerReady()
        {
            StartCoroutine(CountdownStart());
        }

        public void OnGamePaused()
        {
            _isGamePaused = true;
        }

        public void OnGameResumed()
        {
            _isGamePaused = false;
        }

        IEnumerator CountdownStart()
        {
            do
            {
                yield return new WaitUntil(() => !_isGamePaused);
                _countdownText.text = _countdownNumber.ToString();
                yield return new WaitForSeconds(1f);
                _countdownNumber--;

            } while (_countdownNumber > 0);

            OnCountdownEnded?.Invoke();
            gameObject.SetActive(false);
        }

        public void OnGameOver(int index)
        {
            _isGamePaused = true;
        }
    }
}

