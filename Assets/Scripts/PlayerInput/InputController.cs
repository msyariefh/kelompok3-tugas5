using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.PlayerInput
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private InputScriptable inputScriptable;

        public Vector3 ProcessMoveInput()
        {
            if (Input.GetKey(inputScriptable.InputKeys.moveLeft)) return Vector3.left;
            if (Input.GetKey(inputScriptable.InputKeys.moveRight)) return Vector3.right;
            if (Input.GetKey(inputScriptable.InputKeys.moveForward)) return Vector3.forward;
            if (Input.GetKey(inputScriptable.InputKeys.moveBackward)) return Vector3.back;
            else return Vector3.zero;
        }

        public Vector3 ProcessRotateInput()
        {
            if (Input.GetKey(inputScriptable.InputKeys.rotateLeft)) return new Vector3(0, -1, 0);
            if (Input.GetKey(inputScriptable.InputKeys.rotateRight)) return new Vector3(0, 1, 0);
            else return Vector3.zero;
        }
    }
}

