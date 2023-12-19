using Project.Scripts.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Game
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Portal[] connectedPortals;
        
        public float portOffset { private set; get; }
        public Vector3 normalDirection { private set; get; }
        
        public BoxCollider2D BoxCollider2D { private set; get; }

        private void OnEnable()
        {
            normalDirection = VectorUtilities.RotateAroundZ(Vector3.up, transform.rotation.eulerAngles.z);
            BoxCollider2D = GetComponent<BoxCollider2D>();
            portOffset = BoxCollider2D.size.y+ 0.1f;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                TeleportBall(other.gameObject);
            }
        }

        protected virtual void TeleportBall(GameObject ball)
        {
            if (connectedPortals.Length < 1) return;
            Portal connectedPortal = connectedPortals.Length == 1
                ? connectedPortals[0]
                : connectedPortals[Mathf.RoundToInt(Random.Range(0f, connectedPortals.Length - 1f))];

            ball.transform.position = connectedPortal.gameObject.transform.position +
                                      connectedPortal.normalDirection * connectedPortal.portOffset;

            Rigidbody2D ballRb2D = ball.GetComponent<Rigidbody2D>();
            Vector2 velocity = ballRb2D.velocity;

            float entranceAngle = Vector3.Angle(velocity, normalDirection);
            float exitAngle = (entranceAngle % 90 ) *-1;
           
            Vector2 newVelocity =
                VectorUtilities.RotateAroundZ(connectedPortal.normalDirection, exitAngle) * velocity.magnitude;

            ballRb2D.velocity = newVelocity;
        }
    }
}
