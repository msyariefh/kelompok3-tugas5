using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float Delay = 3f;
    //Delay in seconds before destroying the gameobject

    void Start()
    {
        Destroy(gameObject, Delay);
    }
}
