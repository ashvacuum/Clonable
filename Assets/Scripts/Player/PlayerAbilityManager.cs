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
        private List<BaseAbility> _abilities;
        private PlayerCharacterSheet _characterData;

        private void Awake()
        {
            _characterData = new PlayerCharacterSheet(1, 0, 15, 15, 15, 15);
            _abilities = GetComponentsInChildren<BaseAbility>().ToList();
        }

        
    }
}
