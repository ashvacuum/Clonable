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
        private BasicAnimatorController _animController;
        private PlayerAbilityManager _manager;

        private bool _isShiftHeld;

        

        private void Start()
        {
            _setter = GetComponent<DestinationSetter>();
            _navMeshAgentController = GetComponent<NavMeshAgentController>();
            _animController = GetComponent<BasicAnimatorController>();
            _manager = FindAnyObjectByType<PlayerAbilityManager>();

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

            if (isAttack)
            {
                _navMeshAgentController.StopMoving();
                var direction = hit.point - transform.position;
                OnClickedUnit?.Invoke(hit.point, -1);
                
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
                    Debug.Log("Combat Begun");
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
                _navMeshAgentController.ResumeMoving();
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
