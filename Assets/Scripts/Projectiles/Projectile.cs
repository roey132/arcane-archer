using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _damage;
    private AmmoData _ammo;
    private string _effectName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Projectile"))
        {
            if (collision.CompareTag("Enemy")){
                collision.GetComponent<Enemy>().Hit(_damage,"arrow");
                HandleEnemyDebuff(collision.GetComponent<Enemy>());
            }
            if (_effectName != "")
            {
                _ammo.ActivateEffect(transform.position);
            }
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void InitProjectile(AmmoData data)
    {
        _ammo = data;
        _damage = data.Damage;
        _effectName = data.EffectName;
        gameObject.GetComponent<SpriteRenderer>().sprite = data.Sprite;
    }
    public void HandleEnemyDebuff(Enemy enemy)
    {
        print("debuff handler runs");
        if (_ammo.Debuff == null) return;
        print("Sets enemy debuff");
        enemy.SetActiveEffect(_ammo.Debuff);
    }
}
