using UnityEngine;
namespace TankU.Audio
{
    [CreateAssetMenu]
    public class ScriptableSounds : ScriptableObject
    {
        [Header("BGM Name")]
        [SerializeField] private string _mainMenuBGM;

        [Header("Sounds For The Game")]
        [SerializeField] private Sound[] _sounds;

        public string MainMenuBGM => _mainMenuBGM;
        public Sound[] GetSounds()
        {
            Sound[] _tempSoundArray = new Sound[_sounds.Length];
            System.Array.Copy(_sounds, _tempSoundArray, _sounds.Length);
            return _tempSoundArray;
        }
    }
}

