using Player;
using UnityEngine;

namespace Skills
{
    public class SpawnableAbility : EquippedAbility
    {
        [SerializeField] protected CombatActor _projectilePrefab;
        [SerializeField] protected float _abilityRange;

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
            return Vector3.Distance(position, target) <= _abilityRange;
        }

        protected override void ExecuteAbility()
        {
            if (_projectilePrefab == null) return;
            var prefab = Instantiate(_projectilePrefab);
            prefab.Initialize(10, _owner.GetFactionId());
        }
    }
}
