using System;
using System.Collections;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer mySpriteRenderer;

    [SerializeField] private float InputStength = 1, offsetForce =10f;
    [SerializeField] private Color speedUpColor, sizeUpColor;
    private Color normalColor;

    private void OnEnable()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = mySpriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        
        if(Mathf.Abs(input) > .1f) myRigidbody2D.velocity = (Vector2.right * (input * InputStength));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            float offsetScale = (other.transform.position.x - transform.position.x) / transform.localScale.y;
            
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * (offsetScale* offsetForce));
        }
    }
    
    public void TriggerSpeedPlayerPower() { StartCoroutine(SpeedPlayerPowerUp()); }
    private int speedPlayerStackCounter;
    private const int speedPlayermaxStack = 4;
    private IEnumerator SpeedPlayerPowerUp()
    {
        if (speedPlayerStackCounter >= speedPlayermaxStack) yield break;
        
        speedPlayerStackCounter++;
        InputStength *= 1.3f;
        mySpriteRenderer.color = speedUpColor;
        yield return new WaitForSeconds(20f);
        speedPlayerStackCounter--;
        InputStength *= 1/1.3f;
        if(speedPlayerStackCounter < 1 )PowerUpEnded();
    }
    
    public void TriggerGiantPlayerPower() { StartCoroutine(GiantPlayerPowerUp()); }
    private int giantPlayerStackCounter;
    private const int giantPlayermaxStack = 2;
    private IEnumerator GiantPlayerPowerUp()
    {
        if(giantPlayerStackCounter >= giantPlayermaxStack) yield break;
        
        giantPlayerStackCounter++;
        mySpriteRenderer.color = sizeUpColor;
        
        Vector3 scale =transform.localScale;
            scale.y *= 2;
        transform.localScale = scale;
        
        yield return new WaitForSeconds(20f);
        giantPlayerStackCounter--;
        
        scale =transform.localScale;
        scale.y *= 1/2f;
        transform.localScale = scale;
        
        if(giantPlayerStackCounter < 1)PowerUpEnded();
    }


    private void PowerUpEnded()
    {
        if (speedPlayerStackCounter < 1 && giantPlayerStackCounter < 1) mySpriteRenderer.color = normalColor;
    }
        
}
