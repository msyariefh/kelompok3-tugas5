using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TankU.SaveData
{
    public class DataController : MonoBehaviour
    {
        public static DataController Instance { get; private set; }
        [SerializeField] private PlayerProgress[] _playerData;

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
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LoadAllPlayerProgress();
        }

        private void LoadAllPlayerProgress()
        {

        }

        private void GetPlayerProgress(int _id)
        {

        }
    }

}