using System;
using DG.Tweening;
using Input;
using NavmeshMovement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(DestinationSetter), typeof(NavMeshAgentController), typeof(BasicAnimatorController))]
    public class PointAndClickController : MonoBehaviour
    {
        private DestinationSetter _setter;
        private NavMeshAgentController _navMeshAgentController;
        public event Action<Vector3, int> OnClickedUnit;

        private bool _isShiftHeld;

        

        private void Start()
        {
            _setter = GetComponent<DestinationSetter>();
            _navMeshAgentController = GetComponent<NavMeshAgentController>();

            InputManager.Instance.PlayerInput.TopDown.Mouse_Left.performed += PerformLeftClickAction;
            InputManager.Instance.PlayerInput.TopDown.StopMovement_Shift.performed += ctx => { _isShiftHeld = true; };
            InputManager.Instance.PlayerInput.TopDown.StopMovement_Shift.canceled += ctx => { _isShiftHeld = false;};
        }

        private void PerformLeftClickAction(InputAction.CallbackContext ctx)
        {
            PointClickMovement(Mouse.current.position.value, _isShiftHeld);
        }
        private void PointClickMovement(Vector2 point,bool isAttack = false)
        {
            if (Camera.main == null) return;
            var ray = Camera.main.ScreenPointToRay(point);

            if (!Physics.Raycast(ray, out var hit)) return;
            if (!IsPointNavigable(hit.point)) return;
            var location = hit.point;
            if (isAttack)
            {
                _navMeshAgentController.StopMoving();
                OnClickedUnit?.Invoke(hit.point, -1);
                var direction = Vector3.zero;
                try
                {
                    direction = location - this.gameObject.transform.position;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Found Error:  {e.ToString()}");
                }
                Rotate(ref direction);
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

        private bool IsPointNavigable(in Vector3 point, float maxDistance = 1f)
        {
            return NavMesh.SamplePosition(point, out var hit, maxDistance, NavMesh.AllAreas);

        }

        void SetDestination(in Vector3 location)
        {
            if (_setter != null)
            {
                _navMeshAgentController.ResumeMoving();
                _setter.SetDestination(location);
            }
        }
        
        /// <summary>
        /// Alternative method for instant rotation
        /// </summary>
        /// <param name="targetDirection">The direction to face</param>
        public void Rotate(ref Vector3 targetDirection, bool instant = false)
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
