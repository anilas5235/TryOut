using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightColorAsParentColor : MonoBehaviour
{
    private void OnValidate()
    {
        GetComponent<Light2D>().color = GetComponentInParent<SpriteRenderer>().color;
    }
}
