using System;
using NavmeshMovement;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Player
{
    public class EnemyAI : BasicAI
    {
        enum SkeletonState { Wandering, Pursuing, Attacking, Dead }
        SkeletonState aiState = SkeletonState.Wandering;
        
        //wander state variables
       [SerializeField] private float _maxWanderDistance = 15f;
       private Vector3 _startPos;

       private void Start()
       {
           _startPos = transform.position;
           TriggerWandering();
       }

       protected override void RunAI()
        {
            switch (aiState)
            {
                case SkeletonState.Wandering:
                    RunWandering();
                    break;
                case SkeletonState.Pursuing:
                    RunPursuing();
                    break;
                case SkeletonState.Attacking:
                    RunAttacking();
                    break;
                case SkeletonState.Dead:
                    break;
                default:
                    
                    break;
            }
        }
        
        #region Wandering
        void TriggerWandering()
        {
            aiState = SkeletonState.Wandering;
            GetNewWanderDestination();
        }
        void RunWandering()
        {
            var x = _controller.GetDestination().x;
            var y = transform.position.y;
            var z = _controller.GetDestination().z;

            var checkPosition = new Vector3(x, y, z);

            if (Vector3.Distance(transform.position, checkPosition) < 1)
            {
                GetNewWanderDestination();
            }
        }

        void GetNewWanderDestination()
        {
            Debug.Log("Getting new destination");
            float randomX = Random.Range(-_maxWanderDistance, +_maxWanderDistance);
            float randomZ = Random.Range(-_maxWanderDistance, +_maxWanderDistance);
            var newPos = new Vector3(_startPos.x + randomX, _startPos.y,_startPos.z + randomZ);
            _controller.MoveToDestination(newPos);
        }
        #endregion

        #region Pursuing
        void TriggerPursuing()
        {

        }
        void RunPursuing()
        {

        }
        #endregion

        #region Attacking
        void TriggerAttacking()
        {

        }
        void RunAttacking()
        {

        }
        #endregion

        #region Dead

        public override void TriggerDeath()
        {
            base.TriggerDeath();
        }

        #endregion
    }
}
