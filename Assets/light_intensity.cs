using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_intensity : MonoBehaviour
{
    Light myLight;

    [SerializeField]
    private float amplitude = 100;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();   
    }

    // Update is called once per frame
    void Update()
    {
        //         myLight.intensity = Mathf.PingPong(Time.time + 500, 1000);
        myLight.intensity = amplitude * Mathf.Sin(Time.time) + 1;
    }
}
