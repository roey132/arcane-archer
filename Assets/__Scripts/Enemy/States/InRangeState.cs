using Unity.VisualScripting;
using UnityEngine;

public class InRangeState : EnemyState
{
    [SerializeField] private EnemyStats _stats;
    [SerializeField] private EnemyState _attackState;
    [SerializeField] private EnemyState _chaseState;
    [SerializeField] private float _distanceFromWall;
    [SerializeField] private LayerMask _objectsLayerMask;
    public override EnemyState StateBehaviour()
    {
        print("running in range state");
        if (_stats.MaxAttackDistance < _stats.DistanceFromPlayer) return _chaseState;
        if (_stats.NextAttackTime < Time.time) return _attackState;
        return null;
    }
}
