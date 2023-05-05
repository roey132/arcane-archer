using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AmmoSetup : MonoBehaviour
{
    public AmmoData _ammo;
    public SpriteRenderer _spriteRenderer;
    void Start()
    {
    }
    public void Init(AmmoData Ammo)
    {
        _ammo = Ammo;
        _spriteRenderer.sprite = _ammo.sprite;
    }
}
