using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private int _damage;
    private string _effectType;
    private string _effectName;
    private AmmoEffects _effects;
    void Start()
    {
        print($"player transform is {transform.parent.parent.parent}");
        Collider2D playerCollider = transform.parent.parent.parent.gameObject.GetComponent<Collider2D>();
        Collider2D thisCollider = transform.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(thisCollider, playerCollider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.tag);
        if (!collision.CompareTag("Player") && !collision.CompareTag("Projectile"))
        {
            if (collision.CompareTag("Enemy")){
                collision.GetComponent<Enemy>().hit(_damage);
            }
            Debug.Log(collision.tag);
            if (_effectType == "")
            {
                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                _effects = transform.parent.Find("AmmoBehaviour").GetComponent<AmmoEffects>();
                _effects.SetEffectPosition(transform.position);
                _effects.SetEffectName(_effectName);
                _effects.SetEffectType(_effectType);
                _effects.SetParent(transform.parent.gameObject);
                gameObject.SetActive(false);
            }
        }
    }
    public void setDamage(int damage)
    {
        this._damage = damage;
    }
    public void SetEffectType(string effectType)
    {
        this._effectType = effectType;
    }
    public void SetEffectName(string effectName)
    {
        this._effectName = effectName;
    }
}
