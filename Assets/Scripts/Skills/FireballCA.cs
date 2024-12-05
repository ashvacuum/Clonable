using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Skills
{
    public class FireballCA : CombatActor
    {
        [SerializeField] protected float _speed;
        private bool _canMove;
        public override void Initialize(float damageAmount, int factionId)
        {
            base.Initialize(damageAmount, factionId);
            Destroy(this,5f);
            _canMove = true;
        }

        private void Update()
        {
            if(_canMove)
                MoveForward();
        }

        private void MoveForward()
        {
            transform.position += transform.forward * Time.fixedDeltaTime * _speed;
        }
    }
}
