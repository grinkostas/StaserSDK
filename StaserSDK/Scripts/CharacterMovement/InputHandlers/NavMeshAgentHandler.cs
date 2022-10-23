using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StaserSDK;
using UnityEngine.AI;

public class NavMeshAgentHandler : MovementHandler
{
    [SerializeField] private NavMeshAgent _agent;
    private Vector3 _currentDestination = Vector3.zero;

    private Coroutine _currentWaitCoroutine = null;

    public NavMeshAgent Agent => _agent;
    
    protected override Vector3 GetInput()
    {
        return _currentDestination;
    }

    public void SetDestination(Vector3 destination)
    {
        _agent.SetDestination(destination);
        _currentDestination = destination;
        if (_currentWaitCoroutine != null)
            StopCoroutine(_currentWaitCoroutine);
        _currentWaitCoroutine = StartCoroutine(WaitComplete());
        OnStartMove?.Invoke();
    }

    private IEnumerator WaitComplete()
    {
        while (true)
        {
            if (_agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                OnStopMove?.Invoke();
                yield break;
            }
            
            yield return null;
        }
    }
}
