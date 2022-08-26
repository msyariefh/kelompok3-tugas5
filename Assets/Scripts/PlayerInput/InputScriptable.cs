using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.PlayerInput
{
    [CreateAssetMenu]
    public class InputScriptable : ScriptableObject
    {
        [SerializeField] private PlayerInputKey _playerInputKeys;

        public PlayerInputKey InputKeys => _playerInputKeys;
    }
}
