using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    private bool exploded = false;
    public LayerMask levelMask;
    void Start()
    {
        // Invoke Explode in 3 seconds
        Invoke("Explode", 3f);
    }

    void Explode()
    {
        // Create detonate bomb at bomb's position
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Chain of explosion on every direction
        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.left));

        //Disable mesh
        GetComponent<MeshRenderer>().enabled = false;
        exploded = true;

        //Disable collider
        //transform.Find("Collider").gameObject.SetActive(false);

        //Destroy the bomb object in 0.3 seconds, after all coroutines have finished
        Destroy(gameObject, .3f);
    }

    public void OnTriggerEnter(Collider other)
    {
        //If not exploded yet and this bomb is hit by an explosion
        if (!exploded && other.CompareTag("Explosion"))
            //Cancel the already called Explode, else bomb might explode twice
            CancelInvoke("Explode");
            Explode();
    }

    private IEnumerator CreateExplosions (Vector3 direction)
    {
        for (int i = 1; i < 5; i++)
        {
            //gives information about what the raycast hits
            RaycastHit hit;

            //Raycast in the specified direction at i distance, because of the layer mask it'll only hit blocks, not players or bombs
            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit, i, levelMask);

            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation);
            }

            else
            {
                break;
            }

            //Wait 50 milliseconds before checking the next location
            yield return new WaitForSeconds(.05f); 
        }
    }
}