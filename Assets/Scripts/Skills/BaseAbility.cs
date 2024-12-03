using UnityEngine;

namespace Skills
{
    public abstract class BaseAbility : MonoBehaviour
    {
        [SerializeField] private int skillLevel = 0;
        [SerializeField] private string name = "";
        [SerializeField] private string description = "";
        [SerializeField] private Sprite iconSprite;

        public int SkillLevel => skillLevel;
        public string Name => name;
        public string Description => description;
        public Sprite IconSprite => iconSprite;

        public abstract void LevelUp();

        public abstract bool Activate();

        public abstract bool IsInRange(Vector3 position, Vector3 target);
    }
}
