using System.Collections;
using System.Collections.Generic;
using TankU.PlayerInput;
using UnityEngine;

namespace TankU.PlayerObject
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float rotateSpeed;
        [SerializeField]
        private InputController inputController;
        [SerializeField]
        private Transform tankHead;

        private CharacterController controller;


        private void Awake()
        {
            controller = GetComponent<CharacterController>();

        }

        private void FixedUpdate()
        {
            controller.Move(inputController.ProcessMoveInput() * moveSpeed * Time.fixedDeltaTime);
            tankHead.Rotate(inputController.ProcessRotateInput() * rotateSpeed);
            
        }
    }
}

