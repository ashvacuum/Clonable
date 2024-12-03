using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Skills
{
    public class EquippableAbility : BaseAbility
    {
        [SerializeField] protected float _range = 1.5f;

        public override void LevelUp()
        {
        }

        public override bool Activate()
        {
            return true;
        }

        public override bool IsInRange(Vector3 position, Vector3 target)
        {
            return Vector3.Distance(position, target) <= _range;
        }

    }
}
