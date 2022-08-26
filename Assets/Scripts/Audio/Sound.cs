using UnityEngine;
using UnityEngine.Audio;

namespace TankU.Audio
{
    [System.Serializable]
    public class Sound : IClonable
    {
        [SerializeField] private string _name;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private bool _isBGM;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        private AudioSource _audioSource;

        public string Name => _name;
        public AudioClip AudioClip => _audioClip;
        public bool IsBGM => _isBGM;
        public AudioSource AudioSource
        {
            get { return _audioSource; }
            set { _audioSource = value; }
        }
        public AudioMixerGroup AudioMixerGroup => _audioMixerGroup;

        public object Clone()
        {
            var _sound = (Sound)MemberwiseClone();
            _sound.AudioSource = null;
            return _sound;
        }
    }
}

