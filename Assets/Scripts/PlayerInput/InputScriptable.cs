using UnityEngine;

namespace TankU.PlayerInput
{
    [CreateAssetMenu]
    public class InputScriptable : ScriptableObject
    {
        [SerializeField] private PlayerInputKey _playerInputKeys;
        [SerializeField] private KeyCode _pauseInput;

        public PlayerInputKey InputKeys => _playerInputKeys;
        public KeyCode PauseInput => _pauseInput;
    }
}
