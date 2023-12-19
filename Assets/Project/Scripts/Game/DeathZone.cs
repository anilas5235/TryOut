using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.Fail();
        }
    }
}
