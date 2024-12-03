using UnityEngine;
using UnityEngine.Serialization;

namespace Skills
{
    public abstract class BaseAbility : MonoBehaviour
    {
        [SerializeField] protected int skillLevel = 0;
        [FormerlySerializedAs("name")] [SerializeField] protected string _name = "";
        [SerializeField] protected string description = "";
        [SerializeField] protected Sprite iconSprite;
     
        public int SkillLevel => skillLevel;
        public string Name => _name;
        public string Description => description;
        public Sprite IconSprite => iconSprite;

        public abstract void LevelUp();

        public abstract bool Activate(GameObject owner);

        public abstract bool IsInRange(Vector3 position, Vector3 target);
    }
}
