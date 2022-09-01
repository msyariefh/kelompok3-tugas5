using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TankU.HPSystem;
using TankU.SaveData;
using TankU.Audio;

namespace TankU.GameOver
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _winner;
        [SerializeField] private Button MainMenu, Retry;
        [SerializeField] private HPController _hpController;

        private void Start()
        {
            MainMenu.onClick.RemoveAllListeners();
            Retry.onClick.RemoveAllListeners();
            MainMenu.onClick.AddListener(GoMainMenu);
            Retry.onClick.AddListener(GoRetry);
        }

        private void OnEnable()
        {
            _hpController.OnGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            _hpController.OnGameOver += OnGameOver;
        }



        void OnGameOver(int c)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            CheckWin(c);

        }
        
        void CheckWin(int c)
        { 
            if (c == -1)
            {
                for (int i = 0; i < DataController.Instance.TotalPlayer; i++)
                {
                    DataController.Instance.SetPlayerProgress(i, PlayerProgress.MatchResult.TIE);
                }
                _winner.text = "TIE \nToo Bad, No One Wins!";
            }
            else
            {
                for (int i = 0; i <DataController.Instance.TotalPlayer; i++)
                {
                    if (i == c) DataController.Instance.SetPlayerProgress(i, PlayerProgress.MatchResult.WIN);
                    else DataController.Instance.SetPlayerProgress(i, PlayerProgress.MatchResult.LOSE);
                }
                _winner.text = $"Congratulation! \nPlayer {c + 1} Wins!";
            }
        }

        void GoMainMenu() 
        {
            SceneManager.LoadScene("Main Menu");
            AudioManager.Instance.StopAllSFX();
        }

        void GoRetry() 
        {
            SceneManager.LoadScene("Gameplay");
            AudioManager.Instance.StopAllSFX();
        }
    }
}