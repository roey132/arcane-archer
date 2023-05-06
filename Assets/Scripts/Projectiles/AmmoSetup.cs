using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AmmoSetup : MonoBehaviour
{
    public AmmoData _ammo;
    public SpriteRenderer _spriteRenderer;
    public Projectile _projectile;
    void Start()
    {
    }
    public void Init(AmmoData Ammo)
    {
        _ammo = Ammo;
        _projectile.setDamage(_ammo.damage);
        _spriteRenderer.sprite = _ammo.sprite;
    }
}
