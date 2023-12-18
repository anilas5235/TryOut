using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

    [SerializeField] private float startForce =10f;

    private void OnEnable()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D.AddForce(Vector2.up*startForce);
    }
}
