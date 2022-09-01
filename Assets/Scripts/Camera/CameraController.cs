using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera followCamera;
    private Vector3 _cameraPos;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _cameraPos = followCamera.transform.position - transform.position;
    }
    private void LateUpdate()
    {
        //followCamera.transform.position = rb.position + _cameraPos;
    }
}
