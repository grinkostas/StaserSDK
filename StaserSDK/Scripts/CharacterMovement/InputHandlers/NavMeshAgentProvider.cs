using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class NavMeshAgentProvider
{
    public bool enabled
    {
        set
        {
            if(_navMeshAgent != null)
                _navMeshAgent.enabled = value;
        }
    }

    public float speed
    {
        get => _navMeshAgent.speed;
        set => _navMeshAgent.speed = value;
    }

    private NavMeshAgent _navMeshAgent;
        
    public NavMeshAgentProvider(NavMeshAgent navMesh)
    {
        _navMeshAgent = navMesh;
    }
}