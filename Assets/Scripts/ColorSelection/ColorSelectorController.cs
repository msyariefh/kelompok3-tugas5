using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankU.ColorSelection
{
    public class ColorSelectorController : MonoBehaviour
    {
        [SerializeField]
        private GameObject colorView;
        [SerializeField]
        private Button btnPick;
        [SerializeField]
        private GameObject player;

        private Color[] colors;

        private Image colorImage;
        private Renderer cubeRenderer;

        //private int colorIndex;

        private void Start()
        {
            cubeRenderer = player.GetComponent<Renderer>();
            colorImage = colorView.GetComponent<Image>();

            colors = new Color[]
            {
                Color.green,
                Color.blue,
                Color.yellow,
                Color.red
            };
        }

        void OnChangeMaterial()
        {

        }
    }
}