using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace NavmeshMovement
{
    [RequireComponent( typeof(NavMeshAgentController))]
    public class DestinationSetter : MonoBehaviour
    {
       
        private NavMeshAgentController _agentController;

        private void Awake()
        {
            _agentController = GetComponent<NavMeshAgentController>();
        }

        public void SetDestination(Vector3 destination)
        {
            if (_agentController != null)
            {
                _agentController.MoveToDestination(destination);
            }
            else
            {
                Debug.LogWarning("No NavMeshAgentController assigned!");
            }
        }

        // Example method for setting a random destination within a range
        public void SetRandomDestination(float range)
        {
            Vector3 randomDirection = Random.insideUnitSphere * range;
            randomDirection += transform.position;
        
            // Sample a point on the NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
            {
                SetDestination(hit.position);
            }
        }
    }
}