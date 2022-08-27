using System;
using System.Collections;
using System.Collections.Generic;
using TankU.PlayerInput;
using TankU.PlayerObject;
using TankU.Timer;
using UnityEngine;

namespace TankU.Projectile
{
    public class RocketController : MonoBehaviour, IPausable, IHitable
    {
        [SerializeField] private float speed;
        private float _speedInitial;
        private Rigidbody rb;
        //private bool exploded = false;
        [SerializeField] private GameObject explosionPrefab;
        private PauseController _pauseController;
        private PlayerController _playerController;
        public bool _isPoweredUp { get; set; } = false;
        public void SetController(PauseController _pause, PlayerController _player)
        {
            _pauseController = _pause;
            _playerController = _player;
        }

        public event Action<int> OnHitPlayer;

        private void OnEnable()
        {
            _pauseController.OnGamePause += OnGamePaused;
            _pauseController.OnGameResume += OnGameResumed;
            _playerController.OnPowerUpEnded += OnPowerUpEnded;
        }
        private void OnDisable()
        {
            _pauseController.OnGamePause -= OnGamePaused;
            _pauseController.OnGameResume -= OnGameResumed;
            _playerController.OnPowerUpEnded -= OnPowerUpEnded;
        }
        private void OnPowerUpEnded(int _index)
        {
            // Explode?
        }

        public void OnGameOver(int index)
        {
            speed = 0;
        }

        public void OnGamePaused()
        {
            speed = 0;
        }

        public void OnGameResumed()
        {
            speed = _speedInitial;
        }

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            _speedInitial = speed;
            rb.velocity = speed * transform.forward;
            print(transform.position + " " + transform.rotation.ToString());
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamagable _damageInterface = other.gameObject.GetComponent<IDamagable>();

            if(_damageInterface != null)
            {
                OnHitPlayer?.Invoke(_damageInterface.Index);
                gameObject.SetActive(false);
            }

            if (!_isPoweredUp)
            {
                gameObject.SetActive(false);
            }
            
        }
        void Explode()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);


            //Disable mesh
            GetComponent<MeshRenderer>().enabled = false;

            //Destroy the bomb object in 0.3 seconds, after all coroutines have finished
            Destroy(gameObject, .3f);
        }

    }
}
