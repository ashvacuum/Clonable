using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class CombatReceiver : MonoBehaviour, ICombat
    {
        [FormerlySerializedAs("factionID")] [FormerlySerializedAs("FactionID")] [SerializeField]protected int _factionID = 0;

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
            return factionId == _factionID;
        }

        public int GetFactionId()
        {
            return _factionID;
        }
        
        public Vector3 GetSpawnLocation()
        {
            return transform.position + Vector3.up + Vector3.forward * 1f;
        }

        public void SetFactionID(int newID)
        {
            _factionID = newID;
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
