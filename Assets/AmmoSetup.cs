using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AmmoSetup : MonoBehaviour
{
    [SerializeField] private AmmoData _ammo;
    public SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer.sprite = _ammo.sprite;
    }
}
