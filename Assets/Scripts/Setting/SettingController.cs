using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TankU.Audio;


namespace TankU.Setting
{
    public class SettingController : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Button _exitButton;

        private void Awake()
        {
            _exitButton.onClick.AddListener(OnExitButtonClicked);
            _toggle.isOn = AudioManager.Instance.LoadAudioSetting();
            _toggle.onValueChanged.AddListener(OnToggleChanged);
        }
        private void OnToggleChanged(bool _isOn)
        {
            UpdateAudioSetting(_isOn);
        }
        public void UpdateAudioSetting(bool _isMute)
        {
            AudioManager.Instance.UpdateAudioSetting(_isMute);
        }
        public void OnExitButtonClicked()
        {
            gameObject.SetActive(false);
        }
    }
}

