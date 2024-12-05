using System;
using NavmeshMovement;
using Player;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    protected NavMeshAgentController _controller;
    protected bool _alive = true;
    protected int _factionID;
    private void Awake()
    {
        _controller = GetComponent<NavMeshAgentController>();
    }

    public virtual void SetFactionID(int newID)
    {
        _factionID = newID;
        GetComponent<CombatReceiver>().SetFactionID(_factionID);
    }
    
    protected virtual void RunAI()
    {

    }
    
    protected virtual void Update()
    {
        if (_alive) RunAI();
    }

    public virtual void TriggerDeath()
    {
        if (!_alive) return;
        _alive = false;
        if (GetComponent<BasicAnimatorController>() != null)
            GetComponent<BasicAnimatorController>().TriggerDie();

        var attachedColliders = GetComponents<Collider>();
        foreach(var c in attachedColliders)
        {
            c.enabled = false;
        }

        _controller.StopMoving();
    }
}
