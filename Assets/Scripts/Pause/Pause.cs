using TankU.PlayerInput;
using UnityEngine;

namespace TankU.Pause
{

    public class Pause : MonoBehaviour
    {
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private GameObject _pausePopUp;
        
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