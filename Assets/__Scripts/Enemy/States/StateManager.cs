using System.Runtime.CompilerServices;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private EnemyState _startingState;
    [SerializeField] private EnemyState _currState;
    private EnemyState _newState;
    private void Start()
    {
        _currState  = _startingState;
    }
    void Update()
    {
        ActivateCurrentState();
    }
    private void ActivateCurrentState()
    {
        _newState = _currState.StateBehaviour();

        if (_newState != null ) 
        {
            UpdateCurrState(_newState);
        }
    }
    private void UpdateCurrState(EnemyState state)
    {
        _currState = state;
        _newState = null;
        print($"state changed to {state}");
    }
}
