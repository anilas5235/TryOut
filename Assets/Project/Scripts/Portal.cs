using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal connectedPortal;
    
    [SerializeField] public float portOffset =1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
           Transform ballTransform = other.gameObject.transform;

           other.gameObject.transform.position = connectedPortal.gameObject.transform.position + RotateAroundZ(Vector3.up, connectedPortal.transform.rotation.eulerAngles.z)
               * connectedPortal.portOffset;
           other.gameObject.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, ballTransform.rotation.z, transform.rotation.w);
           Rigidbody2D rigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();

           rigidbody2D.velocity = RotateAroundZ(rigidbody2D.velocity, connectedPortal.transform.rotation.eulerAngles.z + transform.rotation.eulerAngles.z) * rigidbody2D.velocity.magnitude*-1;
        }
    }
    
    Vector3 RotateAroundZ(Vector3 start, float angle)
    {
        start.Normalize();
        return Quaternion.AngleAxis(angle, Vector3.forward) * start;
    }
}
