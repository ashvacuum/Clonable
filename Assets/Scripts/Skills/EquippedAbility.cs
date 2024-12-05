using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Skills
{
    public class EquippedAbility : BaseAbility
    {
        [SerializeField] protected float _range = 1.5f;
        
        protected ICombat _owner;
        protected BasicAnimatorController _animator;

        public override void SetupAbility(GameObject owner)
        {
            if (owner.TryGetComponent<ICombat>(out var combatInterface))
            {
                _owner = combatInterface;
                skillLevel = 0;
                if (owner.TryGetComponent<BasicAnimatorController>( out var animator))
                {
                    _animator = animator;
                }
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