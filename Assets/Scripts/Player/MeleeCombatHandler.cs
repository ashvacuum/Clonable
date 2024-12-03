using Skills;
using UnityEngine;

namespace Player
{
    public class MeleeCombatHandler : MonoBehaviour
    {
        [SerializeField] private float attackRadius = 1f;
        [SerializeField] private float attackDistance = 2f;
        [SerializeField] private LayerMask attackLayerMask;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private EquippableAbility LastActivatedAbility;

        public void AttackStart(EquippableAbility ability)
        {
            
        }

        public void ExecuteAttack()
        {
            // Perform the actual attack logic
            var attackOrigin = transform.position + transform.forward;

            var hits = Physics.SphereCastAll(
                attackOrigin,
                attackRadius,
                transform.forward,
                attackDistance,
                attackLayerMask
            );
            if (LastActivatedAbility != null)
            {
            
            }
        
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
