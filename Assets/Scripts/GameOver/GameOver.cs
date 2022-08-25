using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace TankU.GameOver
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _winner;
        [SerializeField] private Button MainMenu, Retry;
    //    [SerializeField] private HPController _hpController;

        private void Start()
        {
            MainMenu.onClick.RemoveAllListeners();
            Retry.onClick.RemoveAllListeners();
            MainMenu.onClick.AddListener(GoMainMenu);
            Retry.onClick.AddListener(GoRetry);
        }

        private void OnEnable()
        {
  //          _hpController.OnGameOver += OnGameOver;   
        }

        private void OnDisable()
        {
    //        _hpController.OnGameOver += OnGameOver;
        }



        void OnGameOver(int c)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            CheckWin(c);
        }
        
        void CheckWin(int c)
        { 
            switch (c)
            {
                case 0:
                    _winner.text = "Congratulation \n Player 1 Wins"; 
                    break;
                case 1:
                    _winner.text = "Congratulation \n Player 2 Wins";
                    break;
                case 2:
                    _winner.text = "Too Bad No One Win";
                    break;

            }
        }

        void GoMainMenu() 
        {
            Debug.Log("MbOH");
//            SceneManager.LoadScene("MainMenu")
        }

        void GoRetry() 
        {
            Debug.Log("Ya Ndak Tau Kok Tanya Saya");
  //          Application.LoadLevel(Application.loadedLevel);
        }
    }
}