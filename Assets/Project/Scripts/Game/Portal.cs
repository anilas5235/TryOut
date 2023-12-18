using Project.Scripts.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Game
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Portal[] connectedPortals;
    
        public float portOffset;

        private void OnEnable()
        {
            portOffset = transform.localScale.y + 0.1f;
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
                : connectedPortals[Random.Range(0, connectedPortals.Length - 1)];

            Vector3 connectedPortalRotation = connectedPortal.transform.rotation.eulerAngles;
            Vector3 myRotation = transform.rotation.eulerAngles;

            ball.transform.position = connectedPortal.gameObject.transform.position +
                                      VectorUtilities.RotateAroundZ(Vector3.up, connectedPortalRotation.z) *
                                      connectedPortal.portOffset;

            Rigidbody2D ballRb2D = ball.GetComponent<Rigidbody2D>();

            ballRb2D.velocity =  VectorUtilities.RotateAroundZ(Vector3.Reflect(ballRb2D.velocity, Vector3.forward),
                myRotation.z - connectedPortalRotation.z) * -1;
        }
    }
}
