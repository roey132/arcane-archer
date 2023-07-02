using System.Collections;
using UnityEngine;

public class AmmoExplosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _effectDamage;
    [SerializeField] private float _effectDurationSeconds;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(FireExplosion());
    }
    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.one * _explosionRadius));
    }
    private IEnumerator FireExplosion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);
        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    print($"the _effect damage is {_effectDamage}");
                    collider.GetComponent<EnemyStats>().Hit(_effectDamage);
                }
            }
        }

        yield return new WaitForSeconds(_effectDurationSeconds);
        Destroy(gameObject);
    }
}
