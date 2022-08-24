using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    //private bool exploded = false;
    public GameObject explosionPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = speed*transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Explode();
        }
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