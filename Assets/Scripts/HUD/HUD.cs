using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TankU.Module.Tutorials;

namespace TankU.Module.HUD
{
    public class HUD : MonoBehaviour
    {
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
        private bool IsPowerUpP1, IsPowerUpP2, IsGameStarted;
        private int _timer;

        private void OnEnable()
        {
            Tutorial.OnGameStart += DisplayPopUp;
        }

        private void OnDisable()
        {
            Tutorial.OnGameStart -= DisplayPopUp;
        }

        void Start()
        {
            GameStart();
            P1slide.GetComponent<Slider>().value = _playeroneHealth;
            P2slide.GetComponent<Slider>().value = _playeroneHealth;

        }
        void OnGameStart()
        {
            IsGameStarted = !IsGameStarted;

        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                OnBouncePowerUpP1();
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                OnBouncePowerUpP2();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                OnPlayerOneHealthChange(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                OnPlayerTwoHealthChange(-1);
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                OnPlayerOneHealthChange(5);
                OnPlayerTwoHealthChange(5);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                OnPowerUpEndedP1();
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                OnPowerUpEndedP2();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                TimerChange(-1);
            }

        }
        void OnBouncePowerUpP1()
        {
            IsPowerUpP1 = true;
            P1PoweredUp();
        }

        void OnBouncePowerUpP2()
        {
            IsPowerUpP2 = true;
            P2PoweredUp();
        }

        void OnPowerUpEndedP1()
        {
            IsPowerUpP1 = false;
            P1PoweredUp();
        }

        void OnPowerUpEndedP2()
        {
            IsPowerUpP2 = false;
            P2PoweredUp();
        }

        void DisplayPopUp()
        {
            HUDPopup.SetActive(true);
        }

        void OnPlayerOneHealthChange(int health)
        {
            _playeroneHealth += health;
            P1slide.GetComponent<Slider>().value = _playeroneHealth;
            CheckHealthP1();
        }

        void OnPlayerTwoHealthChange(int health)
        {
            _playertwoHealth += health;
            P2slide.GetComponent<Slider>().value = _playertwoHealth;
            CheckHealthP2();
        }

        void CheckHealthP1()
        {
            if (_playeroneHealth < 1)
                Debug.Log("Mampus1");
        }

        void CheckHealthP2()
        {
            if (_playertwoHealth < 1)
                Debug.Log("Mampus2");
        }

        void TimerChange(int timechange)
        {
            _timer += timechange;
            StringConverter();
        }

        void GameStart()
        {
            IsGameStarted = true;
            IsPowerUpP1 = false;
            IsPowerUpP2 = false;
            _timer = 240;
            StringConverter();

        }


        void StringConverter()
        {
            string s1 = "00" + Mathf.FloorToInt(_timer / 60).ToString();
            string s2 = "00" + (_timer % 60).ToString();
            _timertext.GetComponent<TextMeshProUGUI>().text = string.Format("{0}:{1}", s1.Substring(s1.Length - 2), s2.Substring(s2.Length - 2));
        }
        void P1PoweredUp()
        {
            if (IsPowerUpP1 == true)
            {
                PowerUpP1.color = new Color(255, 255, 255, 255);
                PowerUpP1.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
            }
            else if (IsPowerUpP1 == false)
            {
                PowerUpP1.color = new Color(255, 255, 255, 150);
                PowerUpP1.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 67);
                Debug.Log("hohihehuha");
            }
        }

        void P2PoweredUp()
        {
            if (IsPowerUpP2 == true)
            {
                PowerUpP2.color = new Color(255, 255, 255, 255);
                PowerUpP2.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
            }

            else if (IsPowerUpP2 == false)
            {
                PowerUpP2.color = new Color(255, 255, 255, 150);
                PowerUpP2.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 67);
                Debug.Log("hahihuheho");
            }


        }


    }

}