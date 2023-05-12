using UnityEngine;

[CreateAssetMenu]
public class SpellAreaDamage : SpellData
{
    public Vector2 _center;
    public float _areaRadius;
    public float _damage;
    public GameObject _effect;

    public override GameObject Activate(Vector2 center, Transform playerTransform)
    {
        // handle colliders and hits
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, _areaRadius);
        foreach (Collider2D collider in colliders){
            if (collider.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().Hit(_damage, "spell");
            }
        }

        // handle effects
        GameObject effect = Instantiate(_effect, center, Quaternion.identity);
        return effect;
    }
}
