using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TankU.Timer;
using TankU.HPSystem;
using TankU.PowerUP;
using TankU.PlayerObject;

namespace TankU.HUD
{
    public class HUD : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TimerController _timerController;
        [SerializeField] private HPController _hpController;
        [SerializeField] private PlayerController[] _playerControllers;
        [SerializeField] private CountdownController _countdownController;

        [Header("Pop-Up")]
        [SerializeField]
        private GameObject HUDPopup;

        [Header("Life")]
        [SerializeField]
        private Slider[] _playerHpSliders;

        [Header("Timer")]
        [SerializeField]
        private TextMeshProUGUI _timertext;

        [Header("PowerUP")]
        [SerializeField]
        private Image[] _playerPowerUpIndicators;

        private List<int> _playerHPs = new();
        private int _timer;

        private void OnEnable()
        {
            _timerController.OnTimerChange += OnTimerChange;
            _hpController.OnPlayerHealthChange += OnHealthPlayerChange;
            foreach(PlayerController _controller in _playerControllers)
            {
                _controller.OnPowerUpStarted += OnBouncePowerUp;
                _controller.OnPowerUpEnded += OnBouncePowerUpEnded;
            }
            _hpController.OnPlayerHealthInit += OnPlayerHealthInit;
            _countdownController.OnCountdownEnded += OnCountdownEnded;

        }

        private void OnDisable()
        {
            _timerController.OnTimerChange -= OnTimerChange;
            _hpController.OnPlayerHealthChange -= OnHealthPlayerChange;
            foreach (PlayerController _controller in _playerControllers)
            {
                _controller.OnPowerUpStarted -= OnBouncePowerUp;
                _controller.OnPowerUpEnded -= OnBouncePowerUpEnded;
                _hpController.OnPlayerHealthInit -= OnPlayerHealthInit;
            }
            _hpController.OnPlayerHealthInit -= OnPlayerHealthInit;
            _countdownController.OnCountdownEnded -= OnCountdownEnded;
        }

        private void OnCountdownEnded()
        {
            HUDPopup.SetActive(true);
        }

        private void OnPlayerHealthInit(List<int> _healths)
        {
            _playerHPs = _healths;
            for (int i = 0; i < _playerHpSliders.Length; i++)
            {
                _playerHpSliders[i].maxValue = _playerHPs[i];
                _playerHpSliders[i].value = _playerHPs[i];
            }
        }

        void OnHealthPlayerChange(int _index, int _health)
        {
            _playerHPs[_index] = _health;
            _playerHpSliders[_index].value = _health;
        }

        void OnTimerChange(int timechange)
        {
            _timer = timechange;
            StringConverter();
        }

        void StringConverter()
        {
            string s1 = "00" + Mathf.FloorToInt(_timer / 60).ToString();
            string s2 = "00" + (_timer % 60).ToString();
            _timertext.GetComponent<TextMeshProUGUI>().text = string.Format("{0}:{1}", s1.Substring(s1.Length - 2), s2.Substring(s2.Length - 2));
        }

        void OnBouncePowerUp(int _index)
        {
            _playerPowerUpIndicators[_index].color = Color.white; ;
            _playerPowerUpIndicators[_index].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }

        void OnBouncePowerUpEnded(int _index)
        {
            _playerPowerUpIndicators[_index].color = new Color(255, 255, 255, 150);
            _playerPowerUpIndicators[_index].GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 255, 255, 67);
        }


    }

}