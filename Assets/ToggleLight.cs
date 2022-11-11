using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{

    public GameObject toggleLight;

    public static bool isLit;

    // Start is called before the first frame update
    void Start()
    {
        toggleLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isLit)
            {
                ToggleOn();
            }
            else
            {
                ToggleOff();
            }
        }
    }

    public void ToggleOn()
    {
        toggleLight.SetActive(true);
        Time.timeScale = 1f;
        isLit = true;
        Debug.Log("Toggle on");
    }

    public void ToggleOff()
    {
        toggleLight.SetActive(false);
        Time.timeScale = 1f;
        isLit = false;
        Debug.Log("Toggle Off");
    }
}
