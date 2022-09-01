using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace TankU.MainMenu
{
    public class HoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TMP_Text _textToBeAnimated;
        private Vector3 _initScale;

        private void Start()
        {
            _initScale = _textToBeAnimated.transform.localScale;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            LeanTween.scale(_textToBeAnimated.gameObject, new Vector3(1.2f, 1.2f, _initScale.z), .15f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            LeanTween.scale(_textToBeAnimated.gameObject, _initScale, .15f);
        }
    }

}

