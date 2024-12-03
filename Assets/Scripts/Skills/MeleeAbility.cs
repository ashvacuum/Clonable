using Player;
using UnityEngine;

namespace Skills
{
    public class MeleeAbility : EquippedAbility
    {
        [SerializeField] private float attackRadius = 1f;
        [SerializeField] private float attackDistance = 1f;
        [SerializeField] private LayerMask attackLayerMask;
        private ICombat _owner;

        public void SetupAbility(GameObject owner)
        {
            if (owner.TryGetComponent<ICombat>(out var combatInterface))
            {
                _owner = combatInterface;
                skillLevel = 0;
            }
        }
    
        public override void LevelUp()
        {
        
        }
    
        public override bool Activate(GameObject owner)
        {
            if (skillLevel <= 0) return false;
            if (!owner.TryGetComponent<CombatHandler>(out var combatHandler)) return false;
            combatHandler.OnPlayerAttackSequenceTriggered += ExecuteAbility;
            return true;
        }


        public override bool IsInRange(Vector3 position, Vector3 target)
        {
            return Vector3.Distance(position, target) <= attackDistance;
        }

        protected override void ExecuteAbility()
        {
            var attackOrigin = transform.position + transform.forward;

            var hits = Physics.SphereCastAll(
                attackOrigin,
                attackRadius,
                transform.forward,
                attackDistance,
                attackLayerMask
            );

            foreach (var hit in hits)
            {
                if (!hit.collider.TryGetComponent<ICombat>(out var combat)) continue;
                if (!combat.IsFriend(_owner.GetFactionId()))
                {
                    combat.TakeDamage(5);
                }
            }
        }
    }
}
