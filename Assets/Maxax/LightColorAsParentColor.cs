using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightColorAsParentColor : MonoBehaviour
{
    [SerializeField] private bool changed;
    private void OnValidate()
    {
        try
        {
            GetComponent<Light2D>().color = GetComponentInParent<SpriteRenderer>().color;
            changed = false;
        }
        catch 
        {
            
        }
        
    }
}
