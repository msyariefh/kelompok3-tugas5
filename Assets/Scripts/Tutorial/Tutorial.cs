using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace TankU.Module.Tutorials
{
    public class Tutorial : MonoBehaviour
    {
        public static event Action OnGameStart;

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

            _next.GetComponent<Button>().onClick.AddListener(buttonchecknext);
            _prev.GetComponent<Button>().onClick.AddListener(buttoncheckprev);
           // popup(true);
        }

        // Update is called once per frame
        void Update()
        {



        }


        void popup( bool pop)
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
        void next()
        {
            _tutorialPos += 1;
            Debug.Log(_tutorialPos);
        }


        void prev()
        {

            _tutorialPos -= 1;
            Debug.Log(_tutorialPos);
        }

        void buttonchecknext()
        {
            if (_tutorialPos == _tutorial.transform.childCount - 1 && OnGameStart != null)
            {
                OnGameStart?.Invoke();
                _tutPopUp.SetActive(false);
            }


            else if (_tutorialPos == _tutorial.transform.childCount - 2)
            {
                _next.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ready";
                next();
                foreach (Transform child in _tutorial.transform)
                    child.gameObject.SetActive(false);

                _tutorial.transform.GetChild(_tutorialPos).gameObject.SetActive(true);



            }
            else if (_tutorialPos >= 0 && _tutorialPos < _tutorial.transform.childCount - 1)
            {
                next();
                foreach (Transform child in _tutorial.transform)
                    child.gameObject.SetActive(false);

                _tutorial.transform.GetChild(_tutorialPos).gameObject.SetActive(true);
                _prev.gameObject.SetActive(true);


            }
            
        }

        void buttoncheckprev() 
        {

            if (_tutorialPos == 1)
            {
                prev();
                _prev.gameObject.SetActive(false);
                foreach (Transform child in _tutorial.transform)
                    child.gameObject.SetActive(false);

                _tutorial.transform.GetChild(_tutorialPos).gameObject.SetActive(true);


            }

            else if (_tutorialPos < _tutorial.transform.childCount && _tutorialPos > 0)
            {
                prev();
                foreach (Transform child in _tutorial.transform)
                    child.gameObject.SetActive(false);

                _tutorial.transform.GetChild(_tutorialPos).gameObject.SetActive(true);
                _next.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next";
            }
        }
    }
}