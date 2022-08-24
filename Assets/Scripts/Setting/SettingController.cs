using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TankU.Audio;


namespace TankU.Setting
{
    public class SettingController : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private Toggle _toggle;
        private bool _isOn;

        private void Awake()
        {
            _audioManager.ChangeSettingObject(gameObject);
            _toggle.isOn = _audioManager.LoadAudioSetting();
            _isOn = _toggle.isOn;
        }

        private void Update()
        {
            if (_toggle.isOn == _isOn) return;
            else
            {
                UpdateAudioSetting(_toggle.isOn);
                _isOn = _toggle.isOn;
            }
        }

        public void UpdateAudioSetting(bool _isOn)
        {
            _audioManager.UpdateAudioSetting(_isOn);
        }

    }
}

