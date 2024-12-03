using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class TopDownCameraController : MonoBehaviour
    {
        [FormerlySerializedAs("target")]
        [Header("Target Settings")]
        [Tooltip("The player to follow")]
        public Transform _target;

        [FormerlySerializedAs("offset")]
        [Header("Camera Positioning")]
        [Tooltip("Offset from the player")]
        public Vector3 _offset = new Vector3(0, 10f, -7f);

        [FormerlySerializedAs("smoothSpeed")]
        [Header("Smoothing")]
        [Tooltip("How quickly the camera moves to the target position")]
        [Range(0.1f, 10f)]
        public float _smoothSpeed = 3f;

        [FormerlySerializedAs("fixedRotation")] [Tooltip("Rotation angle for the camera")]
        public Vector3 _fixedRotation = new Vector3(60f, 0f, 0f);

        [FormerlySerializedAs("useBoundaries")]
        [Header("Camera Boundaries")]
        [Tooltip("Enable camera boundary clamping")]
        public bool _useBoundaries = false;

        [FormerlySerializedAs("minX")] [Tooltip("Minimum X position the camera can reach")]
        public float _minX = -100f;

        [FormerlySerializedAs("maxX")] [Tooltip("Maximum X position the camera can reach")]
        public float _maxX = 100f;

        [FormerlySerializedAs("minZ")] [Tooltip("Minimum Z position the camera can reach")]
        public float _minZ = -100f;

        [FormerlySerializedAs("maxZ")] [Tooltip("Maximum Z position the camera can reach")]
        public float _maxZ = 100f;

        private void Start()
        {
            // If no target is set, try to find the player by tag
            if (_target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    _target = player.transform;
                }
                else
                {
                    Debug.LogError("No player found with 'Player' tag!");
                }
            }

            // Set the initial camera rotation
            transform.rotation = Quaternion.Euler(_fixedRotation);
        }

        private void LateUpdate()
        {
            // Check if we have a valid target
            if (_target == null) return;

            // Calculate desired position with offset
            Vector3 desiredPosition = _target.position + _offset;

            // Apply boundary clamping if enabled
            if (_useBoundaries)
            {
                desiredPosition.x = Mathf.Clamp(desiredPosition.x, _minX, _maxX);
                desiredPosition.z = Mathf.Clamp(desiredPosition.z, _minZ, _maxZ);
            }

            // Smoothly interpolate between current position and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);

            // Update camera position
            transform.position = smoothedPosition;
        }

        // Optional method to immediately snap camera to target
        public void SnapToTarget()
        {
            if (_target != null)
            {
                transform.position = _target.position + _offset;
            }
        }

        // Optional method to change target at runtime
        public void SetTarget(Transform newTarget)
        {
            _target = newTarget;
        }

        // Visualization of camera setup in scene view
        private void OnDrawGizmosSelected()
        {
            if (_target != null)
            {
                // Draw a line from target to camera position
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(_target.position, transform.position);

                // Draw boundary lines if enabled
                if (_useBoundaries)
                {
                    Gizmos.color = Color.red;
                    // Draw X boundaries
                    Vector3 topLeftX = new Vector3(_minX, transform.position.y, _target.position.z);
                    Vector3 bottomRightX = new Vector3(_maxX, transform.position.y, _target.position.z);
                    Gizmos.DrawLine(topLeftX, bottomRightX);

                    // Draw Z boundaries
                    Vector3 topLeftZ = new Vector3(_target.position.x, transform.position.y, _minZ);
                    Vector3 bottomRightZ = new Vector3(_target.position.x, transform.position.y, _maxZ);
                    Gizmos.DrawLine(topLeftZ, bottomRightZ);
                }
            }
        }
    }
}