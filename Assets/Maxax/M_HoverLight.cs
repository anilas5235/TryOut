using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class M_HoverLight : MonoBehaviour
{
    private void OnMouseOver()
    {
        GetComponentInChildren<Light2D>().intensity = 0.7f;
    }

    private void OnMouseExit()
    {
        GetComponentInChildren<Light2D>().intensity = 0f;
    }
}
