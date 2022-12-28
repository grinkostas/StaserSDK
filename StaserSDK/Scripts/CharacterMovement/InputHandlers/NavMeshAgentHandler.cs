using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using StaserSDK;
using UnityEngine.AI;
using Zenject;

public class NavMeshAgentHandler : MovementHandler
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _receiveDestinationDistance;
    [SerializeField] private float _samplePositionRange = 5.0f;
    [SerializeField] private float _updateTime = 0.1f;
    public Vector3 _currentDestination = Vector3.zero;
    public Vector3 _navmeshDestination;

    public List<string> _logs = new List<string>();

    [Inject] private Updater _updater;

    private NavMeshAgentProvider _agentProvider;

    public NavMeshAgentProvider Agent
    {
        get
        {
            if(_agentProvider == null)
                _agentProvider = new NavMeshAgentProvider(_agent);
            return _agentProvider;
        }
    } 
    
    private bool _isMoving = false;
  

    private void OnEnable()
    {
        _updater.Add(this, OnUpdate, _updateTime);
    }

    private void OnDisable()
    {
        _updater.Remove(this);
    }

    public override Vector3 GetInput()
    {
        return _currentDestination;
    }

    private void Update()
    {
        _navmeshDestination = _agent.destination;
    }

    private void OnUpdate()
    {
        if (_isMoving == false)
            return;

        Vector3 destination = _agent.destination;
        Vector3 position = _agent.transform.position;
        
        
        
        if (Vector3.Distance(destination, position) < _receiveDestinationDistance)
        {
            _isMoving = false;
            OnStopMove?.Invoke();
        }
        else
        {
            OnMove?.Invoke(_currentDestination);
        }
        

    }
    
    public void SetDestination(Vector3 destination)
    {
        _logs.Add($"Try Set Destination {destination}");
        if (NavMesh.SamplePosition(destination, out NavMeshHit hit, _samplePositionRange, NavMesh.AllAreas))
        {
            _isMoving = true;
            Agent.enabled = true;
            _agent.SetDestination(hit.position);
            _currentDestination = hit.position;
            
            _logs.Add($"Set {_currentDestination}");
            OnStartMove?.Invoke();
        }
    }

    
}
