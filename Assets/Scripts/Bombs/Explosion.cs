using UnityEngine;

namespace TankU.Bomb
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float _delay = 3f;
        //Delay in seconds before destroying the gameobject

        void Start()
        {
            Destroy(gameObject, _delay);
        }
    }
}

