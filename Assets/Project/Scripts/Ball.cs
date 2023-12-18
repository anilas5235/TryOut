using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    
    [SerializeField] private Vector2 initialDirection = Vector2.up;
    [SerializeField] private float startForce =10f;
    [SerializeField] private float forceIncrease = 1;

    private void OnEnable()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        myRigidbody2D.velocity *= 1 + forceIncrease/100f;
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D.AddForce(initialDirection*startForce);
    }
}
