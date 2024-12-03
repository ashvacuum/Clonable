using System;
using System.Collections.Generic;
using System.Linq;
using Input;
using Skills;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public enum AbilitySlots : Byte
    {
        None,
        LeftClick,
        RightClick,
        Q,
        W,
        E,
        R
    }
    

    public class PlayerAbilityManager : MonoBehaviour
    {
        private Dictionary<AbilitySlots,BaseAbility> _activeAbilities;
        private PlayerCharacterSheet _characterData;
        //Test abilities
        [SerializeField] private BaseAbility _fireball;
        [SerializeField] private BaseAbility _melee;

        private void Awake()
        {
            _characterData = new PlayerCharacterSheet(1, 0, 15, 15, 15, 15);
            _activeAbilities[AbilitySlots.LeftClick] = _melee;
            _activeAbilities[AbilitySlots.RightClick] = _fireball;
            var player = GameObject.FindGameObjectWithTag("Player");

            InputManager.Instance.PlayerInput.TopDown.Mouse_Right.performed +=
                ctx => { _fireball.Activate(player); };

            
        }

        public void RemoveAbility(BaseAbility ability)
        {
            var lastKnownSlot = AbilitySlots.None;
            foreach (var kvp in _activeAbilities)
            {
                if (kvp.Key != AbilitySlots.None && kvp.Value == ability)
                {
                    lastKnownSlot = kvp.Key;
                }
            }

            if (lastKnownSlot != AbilitySlots.None)
            {
                _activeAbilities[lastKnownSlot] = null;
            }
        }

        public void EquipAbility(AbilitySlots slot, BaseAbility ability)
        {
            RemoveAbility(ability);
            _activeAbilities[slot] = ability;
        }

        public bool ActivateAbility(AbilitySlots slot, GameObject owner)
        {
            var hasAbility = _activeAbilities.TryGetValue(slot, out var key) && key != null;

            if (key != null)
            {
                key.Activate(owner);
            }
            
            return hasAbility;
        }
        

        
    }
}
