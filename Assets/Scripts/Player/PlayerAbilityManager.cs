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
        private Dictionary<AbilitySlots,BaseAbility> _activeAbilities = new Dictionary<AbilitySlots, BaseAbility>();
        private PlayerCharacterSheet _characterData;
        //Test abilities
        [SerializeField] private BaseAbility _fireball;
        [SerializeField] private BaseAbility _melee;
        private PointAndClickController _clickController;

        private void Awake()
        {
            _characterData = new PlayerCharacterSheet(1, 0, 15, 15, 15, 15);
            
            
            EquipAbility(AbilitySlots.LeftClick, _melee);
            EquipAbility(AbilitySlots.RightClick, _fireball);
            var player = GameObject.FindGameObjectWithTag("Player");

            InputManager.Instance.PlayerInput.TopDown.Mouse_Right.performed +=
                ctx => { _fireball.Activate(player); };

            if (!player.TryGetComponent<PointAndClickController>(out var controller)) return;
            _clickController = controller;
            _clickController.OnClickedUnit += OnLeftClickDetected;

        }

        private void OnLeftClickDetected(Vector3 point, int factionID)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            _melee.Activate(player);
        }

        public void RemoveAbility(BaseAbility ability)
        {
            var lastKnownSlot = AbilitySlots.None;
            foreach (var kvp in _activeAbilities)
            {
                if (kvp.Value == null) continue;
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
            var player = GameObject.FindGameObjectWithTag("Player");
            ability.SetupAbility(player);
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
