using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TankU.PlayerInput
{
    [System.Serializable]
    public struct PlayerInputKey
    {
        [SerializeField] private KeyCode _moveForward;
        [SerializeField] private KeyCode _moveBackward;
        [SerializeField] private KeyCode _moveLeft;
        [SerializeField] private KeyCode _moveRight;
        [SerializeField] private KeyCode _rotateRight;
        [SerializeField] private KeyCode _rotateLeft;
        [SerializeField] private KeyCode _shoot;
        [SerializeField] private KeyCode _plantBomb;

        public KeyCode moveForward => _moveForward;
        public KeyCode moveBackward => _moveBackward;
        public KeyCode moveLeft => _moveLeft;
        public KeyCode moveRight => _moveRight;
        public KeyCode rotateRight => _rotateRight;
        public KeyCode rotateLeft => _rotateLeft;
        public KeyCode shoot => _shoot;
        public KeyCode plantBomb => _plantBomb;

    }
}

