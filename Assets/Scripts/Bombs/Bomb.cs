using System;
using System.Collections;
using TankU.PlayerInput;
using TankU.PlayerObject;
using TankU.Projectile;
using TankU.Timer;
using UnityEngine;

namespace TankU.Bomb
{
    public class Bomb : MonoBehaviour, IPausable, IHitable
    {
        private PauseController _pauseController;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private LayerMask levelMask;
        [SerializeField] private float _timeBeforeExplode; 
        private bool _isGamePaused = false;

        public event Action<int> OnHitPlayer;

        public void SetPauseController(PauseController _controller)
        {
            _pauseController = _controller;
        }

        private void OnEnable()
        {
            _pauseController.OnGamePause += OnGamePaused;
            _pauseController.OnGameResume += OnGameResumed;
            StartCoroutine(Explode(_timeBeforeExplode));
        }

        private void OnDisable()
        {
            _pauseController.OnGamePause -= OnGamePaused;
            _pauseController.OnGameResume -= OnGameResumed;
        }



        IEnumerator Explode(float _time)
        {
            yield return new WaitUntil(() => !_isGamePaused);
            yield return new WaitForSeconds(_time);
            // Create detonate bomb at bomb's position
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Chain of explosion on every direction
            StartCoroutine(CreateExplosions(Vector3.forward));
            StartCoroutine(CreateExplosions(Vector3.back));
            StartCoroutine(CreateExplosions(Vector3.right));
            StartCoroutine(CreateExplosions(Vector3.left));

            StartCoroutine(DisableObjectAfter(.3f));
        }

        IEnumerator DisableObjectAfter(float _time)
        {
            yield return new WaitForSeconds(_time);
            gameObject.SetActive(false);
        }

        private IEnumerator CreateExplosions(Vector3 direction)
        {
            for (int i = 1; i < 85; i++)
            {
                //gives information about what the raycast hits
                RaycastHit hit;

                //Raycast in the specified direction at i distance, because of the layer mask it'll only hit blocks, not players or bombs
                Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit, i * 5, levelMask);

                if (!hit.collider)
                {
                    Instantiate(explosionPrefab, transform.position + ( 5 *i * direction), explosionPrefab.transform.rotation);
                }

                else
                {
                    Collider _coll = hit.collider;
                    IDamagable _damageInterface = _coll.GetComponentInParent<IDamagable>();
                    if (_damageInterface != null)
                    {
                        OnHitPlayer?.Invoke(_damageInterface.Index);
                    }
                    break;
                }

                //Wait 50 milliseconds before checking the next location
                yield return new WaitForSeconds(.005f);
            }
        }

        public void OnGamePaused()
        {
            _isGamePaused = true;
        }

        public void OnGameResumed()
        {
            _isGamePaused = false;
        }

        public void OnGameOver(int index)
        {
            _isGamePaused = true;
        }
    }
}
