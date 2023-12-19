using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class M_Lightflicker : MonoBehaviour
{
    [SerializeField] private bool OtherWay;

    // Update is called once per frame

    private void Start()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        int rndm = Random.Range(0, 100);
        if (rndm < 30)
        {
            if (OtherWay)
            {
                GetComponent<Light2D>().intensity = 0.01f;
            }
            else
            {
                GetComponent<Light2D>().intensity = 0f; 
            }
            
        }
        else
        {
            if (OtherWay)
            {
                GetComponent<Light2D>().intensity = 0f;
            }
            else
            {
                GetComponent<Light2D>().intensity = 0.01f;
            }
            
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(Flicker());
    }
    
}
