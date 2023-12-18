using UnityEngine;

public class Board : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

    [SerializeField] private float InputStength = 10;

    private void OnEnable()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        
        if(Mathf.Abs(input) > .1f) myRigidbody2D.velocity = (Vector2.right * (input * InputStength));
    }
}
