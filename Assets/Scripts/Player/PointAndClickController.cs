using System;
using System.Drawing;
using DG.Tweening;
using NavmeshMovement;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(DestinationSetter), typeof(NavMeshAgentController), typeof(BasicAnimatorController))]
    public class PointAndClickController : MonoBehaviour
    {
        private DestinationSetter _setter;
        private NavMeshAgentController _navMeshAgentController;
        public event Action<Vector3, int> OnClickedUnit;
        private BasicAnimatorController _animController;
        

        private void Start()
        {
            _setter = GetComponent<DestinationSetter>();
            _navMeshAgentController = GetComponent<NavMeshAgentController>();
            _animController = GetComponent<BasicAnimatorController>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PointClickMovement(Input.GetKey(KeyCode.LeftShift));
            }
        }

        private void PointClickMovement(bool isAttack = false)
        {
            if (Camera.main == null) return;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit)) return;
            if (!IsPointNavigable(hit.point)) return;

            if (isAttack)
            {
                _navMeshAgentController.StopMoving();
                _animController.TriggerAttack();
                var direction = hit.point - transform.position;
                Rotate(direction);
            }
            else
            {

                if (hit.collider.TryGetComponent<ICombat>(out var combatInterface))
                {
                    //combatInterface.IsFriend()
                    OnClickedUnit?.Invoke(hit.point, combatInterface.GetFactionId());
                }
                else
                {
                    SetDestination(hit.point);
                }
            }
        }

        private bool IsPointNavigable(Vector3 point, float maxDistance = 1f)
        {
            return NavMesh.SamplePosition(point, out var hit, maxDistance, NavMesh.AllAreas);

        }

        void SetDestination(Vector3 location)
        {
            if (_setter != null)
            {
                _setter.SetDestination(location);
            }
        }
        
        /// <summary>
        /// Alternative method for instant rotation
        /// </summary>
        /// <param name="targetDirection">The direction to face</param>
        public void Rotate(Vector3 targetDirection, bool instant = false)
        {
            // Ignore Y-axis to keep rotation on the horizontal plane
            targetDirection.y = 0f;

            if (targetDirection == Vector3.zero) return;
            
            var targetRot = Quaternion.LookRotation(targetDirection);
            if (instant)
            {
                // Instantly set rotation to look at the target direction
                transform.rotation = targetRot;
            }
            else
            {
                transform.DORotate(targetRot.eulerAngles, .2f, RotateMode.Fast);
            }
        }

    }
}
