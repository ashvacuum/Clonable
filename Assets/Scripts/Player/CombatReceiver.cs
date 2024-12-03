using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class CombatReceiver : MonoBehaviour, ICombat
    {
        [SerializeField]protected int FactionID = 0;

        [SerializeField] protected float _maxHp = 35;
        protected float CurrentHp = 35;
        protected bool Alive => CurrentHp > 0;
            
        protected virtual void Start()
        {
            CurrentHp = _maxHp;
        }
    
        public virtual void Die()
        {
            
        }

        public bool IsFriend(int factionId)
        {
            return factionId == FactionID;
        }

        public int GetFactionId()
        {
            return FactionID;
        }

        public void SetFactionID(int newID)
        {
            FactionID = newID;
        }
        
        public virtual void TakeDamage(float damage)
        {
            if (!Alive) return;

            CurrentHp -= damage;
            if(CurrentHp <= 0) Die();
        }

        public virtual void Revive()
        {
            
        }
    }
}
