using UnityEngine;


public class ArrowFireExplosion : AbstractOnHitArrowBehaviour
{
    [SerializeField] private int _explosionDamage;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private ArrowData _arrowData;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _arrowObject;
    [SerializeField] private LayerMask _layerMask;

    public override void InitOnHitBehaviour(Vector2 _position)
    {
        transform.position = _position;
        gameObject.SetActive(true);
        DealAreaDamage(_position);
    }

    private void DealAreaDamage(Vector2 _position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_position, _explosionRadius, _layerMask);
        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<EnemyStats>().Hit(_explosionDamage);
                }
            }
        }
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
        ArrowPools.Instance.ReleaseArrow(_arrowData,_arrowObject);
    }
}
