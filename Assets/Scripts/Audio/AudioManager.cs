using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

namespace TankU.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private ScriptableSounds _savedSounds;
        [SerializeField] private AudioMixerGroup _masterAudio;
        private Sound[] _sounds;
        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }

            _sounds = new Sound[_savedSounds.GetSounds().Length];
            System.Array.Copy(_savedSounds.GetSounds(), _sounds, _sounds.Length);

            foreach(Sound _sound in _sounds)
            {
                _sound.AudioSource = gameObject.AddComponent<AudioSource>();
                AudioSourceInit(_sound);
            }
            
        }

        private void Start()
        {
            LoadAudioSetting();
            PlayBGM(_savedSounds.MainMenuBGM); // Only called once when application is opened
        }

        private void UpdateMixerVolume(bool _isMute)
        {
            _masterAudio.audioMixer.SetFloat("Volume", _isMute ? -80f : 0f);
        }

        public bool LoadAudioSetting()
        {
            bool _temp;
            if (!PlayerPrefs.HasKey("_isMute"))
            {
                PlayerPrefs.SetInt("_isMute", 0);
                PlayerPrefs.Save();
            }
            _temp = !(PlayerPrefs.GetInt("_isMute") == 0);
            UpdateMixerVolume(_temp);
            return _temp;
        }

        public void UpdateAudioSetting(bool _isMute)
        {
            if (!PlayerPrefs.HasKey("_isMute"))
            {
                PlayerPrefs.SetInt("_isMute", 0);
                PlayerPrefs.Save();
            }
            PlayerPrefs.SetInt("_isMute", _isMute ? 1 : 0);
            PlayerPrefs.Save();
            UpdateMixerVolume(_isMute);
        }

        private void AudioSourceInit(Sound _sound)
        {
            _sound.AudioSource.clip = _sound.AudioClip;
            _sound.AudioSource.volume = 1.0f;
            _sound.AudioSource.pitch = 1.0f;
            _sound.AudioSource.loop = _sound.IsBGM;
            _sound.AudioSource.playOnAwake = false;
            _sound.AudioSource.outputAudioMixerGroup = _sound.AudioMixerGroup;
        }

        public void PlayBGM(string _bgmName)
        {
            Sound _searchedBGM = System.Array.Find(_sounds, sound => sound.Name == _bgmName 
            && sound.IsBGM);

            if (_searchedBGM?.AudioSource.isPlaying == true) return;

            foreach(Sound s in System.Array.FindAll(_sounds, sound => sound.IsBGM &&
            sound.AudioSource.isPlaying))
            {
                s.AudioSource.Stop();
            }

            _searchedBGM?.AudioSource.Play();
        }

        public void PlaySFX(string _sfxName)
        {
            Sound _searchedSFX = System.Array.Find(_sounds, sound => sound.Name == _sfxName
            && !sound.IsBGM && !sound.AudioSource.isPlaying);

            if (_searchedSFX == null)
            {
                _searchedSFX = (Sound)System.Array.Find(_savedSounds.GetSounds(), sound =>
                sound.Name == _sfxName && !sound.IsBGM)?.Clone();

                _searchedSFX.AudioSource = gameObject.AddComponent<AudioSource>();
                AudioSourceInit(_searchedSFX);
                _sounds = _sounds.Concat(new Sound[] { _searchedSFX }).ToArray();
            }

            _searchedSFX.AudioSource.Play();
        }
    }
}

