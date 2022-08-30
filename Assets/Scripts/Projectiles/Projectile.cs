using System;
using System.Collections;
using System.Collections.Generic;
using TankU.PlayerInput;
using TankU.PlayerObject;
using TankU.Timer;
using UnityEngine;

namespace TankU.Projectile
{
    public class Projectile : MonoBehaviour, IPausable, IHitable
    {
        [SerializeField] private float speed; 
        private Vector3 _velocity;
        private Rigidbody rb;
        private Collider _coll;
        //private bool exploded = false;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private LayerMask _includedMask;
        private PauseController _pauseController;
        private PlayerController _playerController;
        public bool IsPoweredUp { get; set; } = false;
        public void SetController(PauseController _pause, PlayerController _player)
        {
            _pauseController = _pause;
            _playerController = _player;
        }

        public void ChangeRotation(Transform _transform)
        {
            transform.rotation = Quaternion.LookRotation(_transform.forward) * transform.rotation;
            if (rb == null) rb = GetComponent<Rigidbody>();
            rb.velocity = _transform.forward * speed;
        }

        public event Action<int> OnHitPlayer;

        private void OnEnable()
        {
            _pauseController.OnGamePause += OnGamePaused;
            _pauseController.OnGameResume += OnGameResumed;
            _playerController.OnPowerUpEnded += OnPowerUpEnded;
            if (_coll == null) _coll = GetComponent<Collider>();
            _coll.isTrigger = !IsPoweredUp;
        }
        private void OnDisable()
        {
            _pauseController.OnGamePause -= OnGamePaused;
            _pauseController.OnGameResume -= OnGameResumed;
            _playerController.OnPowerUpEnded -= OnPowerUpEnded;
        }
        private void OnPowerUpEnded(int _index)
        {
            Explode();
            gameObject.SetActive(false);
        }

        public void OnGameOver(int index)
        {
            _velocity = rb.velocity;
            rb.velocity = Vector3.zero;
        }

        public void OnGamePaused()
        {
            _velocity = rb.velocity;
            rb.velocity = Vector3.zero;
        }

        public void OnGameResumed()
        {
            rb.velocity = _velocity;
        }

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!(other.CompareTag("Player") || other.CompareTag("Wall"))) return;

            IDamagable _damageInterface = other.GetComponentInParent<IDamagable>();
            
            if(_damageInterface != null)
            {
                print("hit Player " + _damageInterface.Index);
                OnHitPlayer?.Invoke(_damageInterface.Index);
                Explode();
                gameObject.SetActive(false);
            }
            if (!IsPoweredUp)
            {
                Explode();
                gameObject.SetActive(false);
            }
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDamagable _damageInterface = collision.gameObject.GetComponent<IDamagable>();
            if (_damageInterface != null)
            {
                print("hit Player " + _damageInterface.Index);
                OnHitPlayer?.Invoke(_damageInterface.Index);
                Explode();
                gameObject.SetActive(false);
            }
        }
        void Explode()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            GetComponent<MeshRenderer>().enabled = false;

        }

    }
}
