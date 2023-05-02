using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public int numProjectiles;
    public float fireRate;
    public float maxSpreadAngle;
    public float minSpreadAngle;

    private float nextFireTime;
    [SerializeField] private bool canShoot = true;
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            if (canShoot)
            {
                nextFireTime = Time.time + 1f / fireRate;

                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 shootDirection = (mousePosition - (Vector2)transform.position).normalized;
                if (numProjectiles == 1) // Only one projectile, no spread angle

                {
                    GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform);
                    projectile.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
                    projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * projectileSpeed;
                }
                else
                {
                    float spreadAngle = Mathf.Lerp(maxSpreadAngle, minSpreadAngle, (numProjectiles - 1) / 9f);
                    float angleStep = spreadAngle / (numProjectiles - 1);
                    float startAngle = -spreadAngle / 2f;

                    for (int i = 0; i < numProjectiles; i++)
                    {
                        float angle = startAngle + angleStep * i;
                        Vector2 projectileDirection = (Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg) * shootDirection).normalized;
                        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform);
                        projectile.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);
                        projectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * projectileSpeed;
                    }
                }
                
            }
        }
    }
    public void addProjectile()
    {
        numProjectiles++;
    }
    public void addFireRate()
    {
        fireRate++;
    }
}