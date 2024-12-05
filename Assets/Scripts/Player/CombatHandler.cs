using System;
using Skills;
using UnityEngine;

namespace Player
{
    public class CombatHandler : MonoBehaviour
    {
        [SerializeField] private float attackRadius = 1f;
        [SerializeField] private float attackDistance = 1f;
        [SerializeField] private LayerMask attackLayerMask;
        public event Action OnPlayerAttackSequenceTriggered;
        public void AttackStart()
        {
            OnPlayerAttackSequenceTriggered?.Invoke();
            OnPlayerAttackSequenceTriggered = null;
        }

        void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying) return;

            Gizmos.color = Color.red;
            var attackOrigin = transform.position + transform.forward;
            
            Gizmos.DrawWireSphere(attackOrigin + transform.forward * attackDistance, attackRadius);
            Gizmos.DrawLine(attackOrigin, attackOrigin + transform.forward * attackDistance);
        }
    }
}
