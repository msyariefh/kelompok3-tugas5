using TankU.PlayerInput;
using UnityEngine;

namespace TankU.Pause
{

    public class Pause : MonoBehaviour
    {
        [SerializeField] private PauseController _pauseController;
        
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
            transform.GetChild(0).gameObject.SetActive(true);
        }
        void OnGameResumed()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}