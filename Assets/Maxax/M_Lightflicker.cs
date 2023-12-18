using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class M_Lightflicker : MonoBehaviour
{
    

    // Update is called once per frame
    void FixedUpdate()
    {
        int rndm = Random.Range(0, 100);
        if (rndm < 1)
        {
            GetComponent<Light2D>().intensity = 0f;
        }
        else
        {
            GetComponent<Light2D>().intensity = 0.4f;
        }
    }
}
