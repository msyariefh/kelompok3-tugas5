using System.Collections;
using System.Collections.Generic;
using TankU.HPSystem;
using TankU.Timer;
using UnityEngine;

namespace TankU.PlayerInput
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private InputScriptable inputScriptable;
        [SerializeField] private CountdownController countdownController;
        [SerializeField] private HPController hpController;

        private bool isGamePaused = true;

        public void OnGameOver(int index)
        {
            
        }

        private void OnGameStarted()
        {
            isGamePaused = false;
        }

        private void OnEnable()
        {
            countdownController.OnCountdownEnded += OnGameStarted;
            hpController.OnGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            countdownController.OnCountdownEnded -= OnGameStarted;
            hpController.OnGameOver -= OnGameOver;
        }

        public Vector3 ProcessMoveInput()
        {
            if (isGamePaused) return Vector3.zero;
            if (Input.GetKey(inputScriptable.InputKeys.moveLeft)) return Vector3.left;
            if (Input.GetKey(inputScriptable.InputKeys.moveRight)) return Vector3.right;
            if (Input.GetKey(inputScriptable.InputKeys.moveForward)) return Vector3.forward;
            if (Input.GetKey(inputScriptable.InputKeys.moveBackward)) return Vector3.back;
            else return Vector3.zero;
        }

        public Vector3 ProcessRotateInput()
        {
            if (isGamePaused) return Vector3.zero;
            if (Input.GetKey(inputScriptable.InputKeys.rotateLeft)) return new Vector3(0, -1, 0);
            if (Input.GetKey(inputScriptable.InputKeys.rotateRight)) return new Vector3(0, 1, 0);
            else return Vector3.zero;
        }

        private void Update()
        {
            
        }
    }
}

