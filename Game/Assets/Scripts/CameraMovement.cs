using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 offset;
    public GameObject cam;
    void Start()
    {
        cam.transform.position = transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Rotate()
    {
        if (Input.GetMouseButton(1))
        {

        }
    }
}
