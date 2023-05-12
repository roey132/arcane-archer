using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private float _damage;
    private string _effectType;
    private string _effectName;
    private AmmoEffects _effects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Projectile"))
        {
            if (collision.CompareTag("Enemy")){
                collision.GetComponent<Enemy>().Hit(_damage,"arrow");
            }
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
                _effects.SetEffectsDamage(_damage);
                gameObject.SetActive(false);
            }
        }
    }
    public void SetDamage(float damage)
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
