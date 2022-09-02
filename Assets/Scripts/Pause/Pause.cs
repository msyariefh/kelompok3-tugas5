using TankU.Audio;
using TankU.PlayerInput;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TankU.Pause
{

    public class Pause : MonoBehaviour
    {
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private GameObject _pausePopUp;
        [SerializeField] private Button _mainMenuButton;


        private void Start()
        {
            _mainMenuButton.onClick.AddListener(GoMainMenu);
        }

        private void GoMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
            AudioManager.Instance.StopAllSFX();
        }

        private void OnEnable()
        {
            _pauseController.OnGamePause += OnGamePaused;
            _pauseController.OnGameResume += OnGameResumed;
        }
        private void OnDisable()
        {
            _pauseController.OnGamePause -= OnGamePaused;
            _pauseController.OnGameResume -= OnGameResumed;
        }


        void OnGamePaused()
        {
            _pausePopUp.SetActive(true);
            //transform.GetChild(0).gameObject.SetActive(true);
        }
        void OnGameResumed()
        {
            _pausePopUp.SetActive(false);
            //transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}