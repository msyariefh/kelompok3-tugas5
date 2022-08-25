using System;
using System.Collections;
using System.Collections.Generic;
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

        public event Action<int> OnHitPlayer;

        public void OnGameOver()
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
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player1"))
            {
                OnHitPlayer?.Invoke(0);
            }
            else if (other.CompareTag("Player2"))
            {
                OnHitPlayer?.Invoke(1);
            }
            gameObject.SetActive(false);
        }
        void Explode()
        {
            // Create detonate bomb at bomb's position
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Chain of explosion on every direction
            //StartCoroutine(CreateExplosions(Vector3.forward));
            //StartCoroutine(CreateExplosions(Vector3.back));
            //StartCoroutine(CreateExplosions(Vector3.right));
            //StartCoroutine(CreateExplosions(Vector3.left));

            //Disable mesh
            GetComponent<MeshRenderer>().enabled = false;
            //exploded = true;

            //Disable collider
            //transform.Find("Collider").gameObject.SetActive(false);

            //Destroy the bomb object in 0.3 seconds, after all coroutines have finished
            Destroy(gameObject, .3f);
        }

    }
}
