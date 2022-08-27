using UnityEngine.SceneManagement;
using TankU.Audio;
using UnityEngine.UI;
using UnityEngine;

namespace TankU.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private GameObject _settingUI;

        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }
        private void Start()
        {
            AudioManager.Instance.PlayBGM("MainMenuBGM");
        }

        private void OnPlayButtonClicked()
        {
            // Change to gameplay scene
            SceneManager.LoadScene("Gameplay");
        }

        private void OnSettingsButtonClicked()
        {
            _settingUI.SetActive(true);
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }

}
