using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AmmoEffects : MonoBehaviour
{
    private string _effectType;
    private string _effectName;
    private Vector2 _effectPosition;
    private int _effectDamage = 1;
    private float _effectDuration = 0.1f;
    private GameObject _parent;

    void Update()
    {
        if (_effectType == null) return;

        if (_effectType == "Explosion")
        {
            _effectType = null;
            if (_effectName == "FireExplosion")
            {
                _effectName = null;
                StartCoroutine(FireExplosion());
            }
            if (_effectName == "IceExplosion")
            {
                _effectName = null;
                StartCoroutine(IceExplosion());
            }
        }
    }
    private IEnumerator FireExplosion()
    {
        GameObject explosionPrefab = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Prefabs/Explosion.prefab");
        GameObject explosion = Instantiate(explosionPrefab, _effectPosition,Quaternion.identity);
        explosion.GetComponent<SpriteRenderer>().color = new Color(230,0,0,0.6f);

        float explosionRadius = explosion.transform.localScale.x / 2;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosion.transform.position, explosionRadius);

        if (colliders.Length > 0) 
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<Enemy>().hit(_effectDamage);
                }
            }
        }
        
        yield return new WaitForSeconds(_effectDuration);
        Destroy(explosion);
        _parent.SetActive(false);
    }
    private IEnumerator IceExplosion()
    {
        GameObject explosionPrefab = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Prefabs/Explosion.prefab");
        GameObject explosion = Instantiate(explosionPrefab, _effectPosition, Quaternion.identity);
        explosion.GetComponent<SpriteRenderer>().color = new Color(0, 250, 250, 0.3f);

        float explosionRadius = explosion.transform.localScale.x / 2;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosion.transform.position, explosionRadius);

        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<Enemy>().hit(_effectDamage);
                }
            }
        }

        yield return new WaitForSeconds(_effectDuration);
        Destroy(explosion);
        transform.parent.gameObject.SetActive(false);
    }
    public void SetEffectName(string effectName)
    {
        _effectName = effectName;
    }
    public void SetEffectType(string effectType)
    {
        _effectType = effectType;
    }
    public void SetEffectPosition(Vector2 position)
    {
        _effectPosition = position;
    }
    public void SetParent(GameObject parentTransform)
    {
        _parent = parentTransform;
    }
}
