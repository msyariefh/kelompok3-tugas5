using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TankU.ColorSelection
{
    public class PanelColorSelector : MonoBehaviour
    {
        [SerializeField]
        private Button btnNext;

        private void Awake()
        {
            btnNext.onClick.AddListener(OnSelection);
        }

        private void OnSelection()
        {
            gameObject.SetActive(false);
        }
    }
}

