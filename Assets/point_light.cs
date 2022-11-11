using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point_light : MonoBehaviour
{
    public GameObject freelook_camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 look = freelook_camera.transform.forward;

 //       Vector3 relativePos = freelook_camera.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(look, Vector3.up);

        transform.rotation = rotation;

    }
}
