using Player;
using Unity.Mathematics;
using UnityEngine;

namespace Skills
{
    public class SpawnableAbility : EquippedAbility
    {
        [SerializeField] protected CombatActor _projectilePrefab;
        

        public override void LevelUp()
        {
        }

        public override bool Activate(GameObject owner)
        {
            if (skillLevel <= 0) return false;
            if (!owner.TryGetComponent<CombatHandler>(out var combatHandler))
            {
                Debug.LogError("Cannot find valid ability");
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
            if (_projectilePrefab == null) return;
            var prefab = Instantiate(_projectilePrefab, _owner.GetSpawnLocation(), _owner.GetActorRotation());
            prefab.Initialize(10, _owner.GetFactionId());
        }
    }
}
