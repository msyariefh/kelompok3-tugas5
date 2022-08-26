using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace TankU.Tutorials
{
    public class Tutorial : MonoBehaviour
    {
        public event Action OnPlayerReady;

        [Header("Tutorial Panel")]
        [SerializeField]
        private Button _next;
        [SerializeField]
        private Button _prev;
        [SerializeField]
        private GameObject _tutPopUp;
        [SerializeField]
        private GameObject _tutorial;


        private int _tutorialPos  = 0;
        private bool _isTutorial = false;

        // Start is called before the first frame update
        void Start()
        {

            _next.GetComponent<Button>().onClick.AddListener(ButtonCheckNext);
            _prev.GetComponent<Button>().onClick.AddListener(ButtonCheckPrev);
           // popup(true);
        }

        void PopUp( bool pop)
        {
            _isTutorial = pop;
            if (_isTutorial)
            {
                _tutPopUp.SetActive(true);
                _tutorialPos = 0;
                _prev.gameObject.SetActive(false);
            }
            else
                _tutPopUp.SetActive(false);
        }
        void Next()
        {
            _tutorialPos += 1;
            Debug.Log(_tutorialPos);
        }


        void Prev()
        {

            _tutorialPos -= 1;
            Debug.Log(_tutorialPos);
        }

        void ButtonCheckNext()
        {
            if (_tutorialPos == _tutorial.transform.childCount - 1)
            {
                OnPlayerReady?.Invoke();
                _tutPopUp.SetActive(false);
            }


            else if (_tutorialPos == _tutorial.transform.childCount - 2)
            {
                _next.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ready";
                Next();
                foreach (Transform child in _tutorial.transform)
                    child.gameObject.SetActive(false);

                _tutorial.transform.GetChild(_tutorialPos).gameObject.SetActive(true);



            }
            else if (_tutorialPos >= 0 && _tutorialPos < _tutorial.transform.childCount - 1)
            {
                Next();
                foreach (Transform child in _tutorial.transform)
                    child.gameObject.SetActive(false);

                _tutorial.transform.GetChild(_tutorialPos).gameObject.SetActive(true);
                _prev.gameObject.SetActive(true);


            }
            
        }

        void ButtonCheckPrev() 
        {

            if (_tutorialPos == 1)
            {
                Prev();
                _prev.gameObject.SetActive(false);
                foreach (Transform child in _tutorial.transform)
                    child.gameObject.SetActive(false);

                _tutorial.transform.GetChild(_tutorialPos).gameObject.SetActive(true);


            }

            else if (_tutorialPos < _tutorial.transform.childCount && _tutorialPos > 0)
            {
                Prev();
                foreach (Transform child in _tutorial.transform)
                    child.gameObject.SetActive(false);

                _tutorial.transform.GetChild(_tutorialPos).gameObject.SetActive(true);
                _next.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next";
            }
        }
    }
}