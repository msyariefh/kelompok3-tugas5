using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TankU.Timer;
using TankU.HPSystem;

namespace TankU.HUD
{
    public class HUD : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TimerController _timerController;
        [SerializeField] private HPController _hpController;

        [Header("Pop-Up")]
        [SerializeField]
        private GameObject HUDPopup;

        [Header("Life")]
        [SerializeField]
        private Slider P1slide, P2slide;

        [Header("Timer")]
        [SerializeField]
        private TextMeshProUGUI _timertext;

        [Header("PowerUP")]
        [SerializeField]
        private Image PowerUpP1;
        [SerializeField]
        private Image PowerUpP2;

        private int _playeroneHealth, _playertwoHealth;
        private int _timer;

        private void OnEnable()
        {
            _timerController.OnTimerChange += OnTimerChange;
            _hpController.OnPlayerHealthChange += OnHealthPlayerChange;

        }

        private void OnDisable()
        {
            _timerController.OnTimerChange -= OnTimerChange;
            _hpController.OnPlayerHealthChange -= OnHealthPlayerChange;
        }

        void Start()
        {
            P1slide.GetComponent<Slider>().value = _playeroneHealth;
            P2slide.GetComponent<Slider>().value = _playeroneHealth;

        }

        void OnHealthPlayerChange(int _index, int _health)
        {
            switch (_index)
            {
                case 0:
                    _playeroneHealth = _health;
                    P1slide.GetComponent<Slider>().value = _playeroneHealth;
                    break;
                case 1:
                    _playertwoHealth = _health;
                    P2slide.GetComponent<Slider>().value = _playertwoHealth;
                    break;
            }
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
            switch (_index)
            {
                case 0:
                    PowerUpP1.color = Color.white;
                    PowerUpP1.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                    break;
                case 1:
                    PowerUpP2.color = Color.white;
                    PowerUpP2.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                    break;
            }
        }

        void OnBouncePowerUpEnded(int _index)
        {
            switch (_index)
            {
                case 0:
                    PowerUpP1.color = new Color(255, 255, 255, 150);
                    PowerUpP1.GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 255, 255, 67);
                    break;
                case 1:
                    PowerUpP2.color = new Color(255, 255, 255, 150);
                    PowerUpP2.GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 255, 255, 67);
                    break;
            }
        }


    }

}