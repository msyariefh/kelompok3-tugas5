using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankU.ColorSelection
{
    public class ColorSelector : MonoBehaviour
    {
        [SerializeField]
        private GameObject colorView;
        [SerializeField]
        private Button btnPick;
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private Color[] colors;

        private Image colorImage;
        private Renderer cubeRenderer;

        private int colorIndex;

        private void Awake()
        {
            cubeRenderer = player.GetComponent<Renderer>();
            colorImage = colorView.GetComponent<Image>();
            btnPick.onClick.AddListener(ChangeMaterial);
        }

        public void ChangeMaterial()
        {
            colorIndex++;
            if(colorIndex>3)
            {
                colorIndex = 0;
            }

            colorImage.color = colors[colorIndex];
            cubeRenderer.material.color = colors[colorIndex];
        }
    }
}


