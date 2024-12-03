using UnityEngine;
using UnityEngine.AI;

namespace NavmeshMovement
{
    public class NavMeshAgentController : MonoBehaviour
    {
        // Reference to the NavMeshAgent component
        private NavMeshAgent _agent;

        private void Start()
        {
            // Get the NavMeshAgent component attached to this GameObject
            _agent = GetComponent<NavMeshAgent>();

            // Check if NavMeshAgent is found
            if (_agent == null)
            {
                Debug.LogError("NavMeshAgent component not found on this GameObject!");
            }
        }

        // Method to move the agent to a specific destination
        public void MoveToDestination(Vector3 destination)
        {
            if (_agent != null)
            {
                _agent.SetDestination(destination);
            }
            else
            {
                Debug.LogWarning("Cannot move: NavMeshAgent is not initialized!");
            }
        }

        // Optional: Method to stop the agent
        public void StopMoving()
        {
            if (_agent != null)
            {
                _agent.isStopped = true;
            }
        }

        // Optional: Method to resume movement
        public void ResumeMoving()
        {
            if (_agent != null)
            {
                _agent.isStopped = false;
            }
        }

        // Optional: Check if the agent has reached the destination
        public bool HasReachedDestination()
        {
            if (_agent != null)
            {
                return !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;
            }
            return false;
        }
    }
}
