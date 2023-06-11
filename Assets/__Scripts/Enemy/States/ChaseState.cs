using UnityEngine;

public class ChaseState : EnemyState
{
    [SerializeField] private EnemyStats _stats;
    [SerializeField] private EnemyState _inAttackRangeState;
    public override EnemyState StateBehaviour()
    {
        print("running chase state");
        if (_stats.DistanceFromPlayer <= _stats.MaxAttackDistance) return _inAttackRangeState;
        MoveTowardsPlayer();
        return null;
    }
    private void MoveTowardsPlayer()
    {
        _stats.Self.position = Vector2.MoveTowards(_stats.Self.position, _stats.Player.position, _stats.CurrMovementSpeed * Time.deltaTime);
    }
}
