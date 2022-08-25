using System.Collections;
using System.Collections.Generic;
using TankU.Timer;
using UnityEngine;

namespace TankU.Projectile
{
    public class LaunchProjectile : MonoBehaviour
    {
        public GameObject projectile;
        private Rigidbody rb;


        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject cannonball = Instantiate(projectile, transform.position, transform.rotation);
            }
        }
    }
}

