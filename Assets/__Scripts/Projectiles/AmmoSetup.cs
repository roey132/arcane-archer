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
        _projectile.InitProjectile(Ammo);
    }
}
