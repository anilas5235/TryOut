using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal[] connectedPortals;
    
    [SerializeField] public float portOffset =1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            TeleportBall(other.gameObject);
        }
    }

    protected virtual void TeleportBall(GameObject ball)
    {
        Portal connectedPortal;
        if(connectedPortals.Length < 1) return;
        connectedPortal = connectedPortals.Length == 1 ? connectedPortals[0] : connectedPortals[Random.Range(0, connectedPortals.Length - 1)];
           
        ball.transform.position = connectedPortal.gameObject.transform.position +
                                  RotateAroundZ(Vector3.up, connectedPortal.transform.rotation.eulerAngles.z) *
                                  connectedPortal.portOffset;

        Rigidbody2D ballRb2D = ball.GetComponent<Rigidbody2D>();

        ballRb2D.velocity = RotateAroundZ(ballRb2D.velocity, connectedPortal.transform.rotation.eulerAngles.z + transform.rotation.eulerAngles.z) *
                            ballRb2D.velocity.magnitude * -1;
    }

    Vector3 RotateAroundZ(Vector3 start, float angle)
    {
        start.Normalize();
        return Quaternion.AngleAxis(angle, Vector3.forward) * start;
    }
}
