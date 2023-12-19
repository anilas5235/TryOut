using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private bool kickStart;
    public Board player;
    
    [SerializeField] private Vector2 initialDirection = Vector2.up;
    [SerializeField] private float startForce =10f;
    [SerializeField] private float forceIncrease = 1;
    [SerializeField] private bool releaseBallOnStart;

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
        if(releaseBallOnStart) ReleaseBall();
    }

    private void Update()
    {
        if(!kickStart) return;

        if (Input.GetButtonDown("Jump")) ReleaseBall();
    }

    public void ReleaseBall()
    {
         transform.SetParent(null);
         myRigidbody2D.isKinematic = false;
         myRigidbody2D.AddForce(initialDirection*startForce);
    }

    public void BallReset()
    {
        myRigidbody2D.velocity = Vector2.zero;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, 0);
        transform.SetParent(player.transform);
        myRigidbody2D.isKinematic = true;
        kickStart = true;
    }
    
    public void TriggerReckingBallPower() { StartCoroutine(ReckingBallPowerUp()); }
    private IEnumerator ReckingBallPowerUp()
    {
        Transform T = transform;
        T.localScale *= 1.5f;
        yield return new WaitForSeconds(20f);
        T.localScale *= 1/1.5f;
    }
}
