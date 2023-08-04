using UnityEngine;
using UnityEngine.UIElements;

public class PatrolState : EnemyState
{
    [SerializeField] private EnemyState _attackState;
    [SerializeField] private float _pointGenerationCooldown;
    [SerializeField] private EnemyStats _stats;

    private Vector2 _targetLocation;
    private float _nextPointGenerationTime;
    private Transform _spawnArea;

    public void Awake()
    {
        _spawnArea = GameObject.FindGameObjectWithTag("SpawnArea").transform;
        _nextPointGenerationTime = 0;
    }


    public override EnemyState StateBehaviour()
    {

        if (_stats.NextAttackTime < Time.time & _stats.AttackRange >= _stats.DistanceFromPlayer) return _attackState;

        GetDestinationPoint();
        WalkTowardPoint();

        return null;
    }

    private void OnDrawGizmosSelected()
    {
        if (enabled)
        ShowDirectionDebugRay();
    }

    public void GetDestinationPoint()
    {
        if (Time.time < _nextPointGenerationTime) return;
        _targetLocation = PointGenerator.GetRandomPointInArea(_stats.Self.position, 50, _spawnArea.GetComponent<Collider2D>());
        print(_targetLocation);
        _nextPointGenerationTime = Time.time + _pointGenerationCooldown * Random.Range(0.7f, 1f);
    }
    public void WalkTowardPoint()
    {
        print("walking to target location");
        _stats.Self.position = Vector2.MoveTowards(_stats.Self.position, _targetLocation, _stats.ChaseMovementSpeed * _stats.MovementSpeedModifier * Time.deltaTime);
    }

    public void ShowDirectionDebugRay()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_stats.Self.position, _targetLocation);
    }
}
