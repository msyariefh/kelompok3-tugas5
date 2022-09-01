using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace TankU.MainMenu
{
    public class HoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Vector3 _initScale;

        private void Start()
        {
            _initScale = transform.localScale;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            LeanTween.scale(gameObject, new Vector3( _initScale.x * 1.25f, _initScale.y * 1.25f, _initScale.z), .15f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            LeanTween.scale(gameObject, _initScale, .15f);
        }
    }

}

