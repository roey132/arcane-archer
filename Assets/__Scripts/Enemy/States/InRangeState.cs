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
        WallDetection();
        if (_stats.MaxAttackDistance < _stats.DistanceFromPlayer) return null;
        if (_stats.NextAttackTime < Time.time) return null;
        return null;
    }
    private void MaintainDistanceFromPlayer()
    {
        _stats.Self.position = Vector2.MoveTowards(_stats.Self.position, (Vector2)_stats.Player.position - _stats.DirectionToPlayer.normalized * _stats.MaintainAttackDistance, _stats.InRangeMovementSpeed * _stats.MovementSpeedModifier * Time.deltaTime);
    }
    private void WallDetection()
    {
        RaycastHit2D leftRay = Physics2D.Raycast(_stats.Self.position, Vector2.left, _distanceFromWall, _objectsLayerMask);
        RaycastHit2D rightRay = Physics2D.Raycast(_stats.Self.position, Vector2.right, _distanceFromWall, _objectsLayerMask);
        RaycastHit2D upRay = Physics2D.Raycast(_stats.Self.position, Vector2.up, _distanceFromWall, _objectsLayerMask);
        RaycastHit2D downRay = Physics2D.Raycast(_stats.Self.position, Vector2.down, _distanceFromWall, _objectsLayerMask);


        Collider2D upDownCollider = null;
        Collider2D leftRightCollider = null;

        if (leftRay.collider != null) upDownCollider = leftRay.collider;
        if (rightRay.collider != null) upDownCollider = rightRay.collider;
        if (leftRay.collider != null) leftRightCollider = upRay.collider;
        if (rightRay.collider != null) leftRightCollider = downRay.collider;

        if (upDownCollider != null & CloseToPlayer())
        {
            MoveUpDown();
            return;
        }
        if (leftRightCollider != null & CloseToPlayer())
        {
            MoveLeftRight();
            return;
        }

        MaintainDistanceFromPlayer();
    }
    private void MoveUpDown()
    {
        Vector2 moveDir = new Vector2(0f, Mathf.Sign(_stats.Self.position.y - _stats.Player.position.y));
        _stats.Self.position = Vector2.MoveTowards(_stats.Self.position, (Vector2)_stats.Self.position + moveDir, _stats.InRangeMovementSpeed * _stats.MovementSpeedModifier * Time.deltaTime);
    }
    private void MoveLeftRight()
    {
        Vector2 moveDir = new Vector2(Mathf.Sign(_stats.Self.position.x - _stats.Player.position.x), 0f) ;
        _stats.Self.position = Vector2.MoveTowards(_stats.Self.position, (Vector2)_stats.Self.position + moveDir, _stats.InRangeMovementSpeed * _stats.MovementSpeedModifier * Time.deltaTime);
    }
    private bool CloseToPlayer()
    {
        return Vector2.Distance(_stats.Self.position, _stats.Player.position) < _stats.MaintainAttackDistance;
    }
}
