using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private int hp =1;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball")) BrickHit();
    }

    private void BrickHit()
    {
        hp--;
        if(hp>0)return;
        
        Destroy(gameObject);
    }
}
