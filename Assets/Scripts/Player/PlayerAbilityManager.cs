using System;
using System.Collections.Generic;
using System.Linq;
using Skills;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerAbilityManager : MonoBehaviour
    {
        private Dictionary<KeyCode, BaseAbility> _equippedAbilities;
        private PlayerCharacterSheet _characterData;

        private void Awake()
        {
            _characterData = new PlayerCharacterSheet(1, 0, 15, 15, 15, 15);
        }

        private void Update()
        {
            foreach (var kvp in _equippedAbilities.Where(kvp => Input.GetKeyDown(kvp.Key)))
            {
                kvp.Value.Activate();
            }
        }

        public void AddAbility(KeyCode key, BaseAbility ability)
        {
            _equippedAbilities[key] = ability;
        }

        public void RemoveAbility(BaseAbility ability)
        {
            
        }
    }
}
