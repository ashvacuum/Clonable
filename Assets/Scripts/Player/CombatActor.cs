using System;
using UnityEngine;

namespace Player
{
    public class CombatActor : MonoBehaviour
    {
        protected int factionID = 0;
        protected float damage = 1;

        public virtual void Initialize(float damageAmount, int factionId)
        {
            this.damage = damageAmount;
            this.factionID = factionId;
        }

        protected virtual void HitReceiver(ICombat target)
        {
            target.TakeDamage(damage);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<ICombat>(out var validTarget) || other.isTrigger) return;
            
            if (!validTarget.IsFriend(factionID))
            {
                HitReceiver(validTarget);
            }
        }
    }
}
