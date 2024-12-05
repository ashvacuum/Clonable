using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Skills
{
    public class MeleeAbility : EquippedAbility
    {
        [FormerlySerializedAs("attackRadius")] [SerializeField] private float _attackRadius = 1f;
        [FormerlySerializedAs("attackLayerMask")] [SerializeField] private LayerMask _attackLayerMask;
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
            if (!owner.TryGetComponent<CombatHandler>(out var combatHandler))
            {
                Debug.LogError("No combat handler found");
                return false;
            }
            combatHandler.OnPlayerAttackSequenceTriggered += ExecuteAbility;
            combatHandler.AttackStart();
            return true;
        }


        public override bool IsInRange(Vector3 position, Vector3 target)
        {
            return Vector3.Distance(position, target) <= _range;
        }

        protected override void ExecuteAbility()
        {
            var attackOrigin = transform.position + transform.forward;

            var hits = Physics.SphereCastAll(
                attackOrigin,
                _attackRadius,
                transform.forward,
                _range,
                _attackLayerMask
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
