using UnityEngine;

namespace Player
{
    public class Clickable : MonoBehaviour
    {
        
    }
    
    public interface IInteractable
    {
        void Interact();
    }

    public interface ICombat
    {
        void TakeDamage(float damage);
        void Die();
        bool IsFriend(int factionId);

        int GetFactionId();

        Vector3 GetSpawnLocation();

        Quaternion GetActorRotation();
    }
}


