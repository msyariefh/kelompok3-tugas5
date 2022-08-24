using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePowerUp : MonoBehaviour
{
    public GameObject bouncepowerFX;
    public event Action OnBouncePowerUp;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnBouncePowerUp?.Invoke();
        }
    }

   
}
