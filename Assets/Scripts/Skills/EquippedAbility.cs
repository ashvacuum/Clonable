using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Skills
{
    public class EquippedAbility : BaseAbility
    {
        [SerializeField] protected float _range = 1.5f;
        
        protected ICombat _owner;

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
            return true;
        }

        public override bool IsInRange(Vector3 position, Vector3 target)
        {
            return Vector3.Distance(position, target) <= _range;
        }

        protected virtual void ExecuteAbility()
        {
            
        }

    }
}
