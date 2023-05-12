using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AmmoSetup : MonoBehaviour
{
    public AmmoData _ammo;
    public SpriteRenderer _spriteRenderer;
    public Projectile _projectile;

    public void Init(AmmoData Ammo)
    {
        _ammo = Ammo;
        _projectile.gameObject.SetActive(true);
        _projectile.SetDamage(_ammo.damage);
        _projectile.SetEffectName(_ammo.effectName);
        _projectile.SetEffectType(_ammo.effectType);
        _spriteRenderer.sprite = _ammo.sprite;
    }
}
