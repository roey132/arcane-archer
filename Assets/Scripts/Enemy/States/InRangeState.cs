using UnityEngine;

public class InRangeState : EnemyState
{
    [SerializeField] private MeleeEnemyStats _stats;
    [SerializeField] private EnemyState _attackState;
    [SerializeField] private EnemyState _chaseState;
    public override EnemyState StateBehaviour()
    {
        print("running in range state");
        if (_stats.MaxAttackDistance < _stats.DistanceFromPlayer) return _chaseState;
        if (_stats.NextAttackTime < Time.time) return _attackState;
        MaintainDistanceFromPlayer();
        return null;
    }
    private void MaintainDistanceFromPlayer()
    {
        print("this should run");
        _stats.Self.position = Vector2.MoveTowards(_stats.Self.position, (Vector2)_stats.Player.position - _stats.DirectionToPlayer.normalized * _stats.MaintainAttackDistance, _stats.CurrMovementSpeed * 0.7f * Time.deltaTime);
    }
}
