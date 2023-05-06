using UnityEditor;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public ingameStats Stats;
    public float MaxSpreadAngle;
    public float MinSpreadAngle;

    private string _equippedArrowName = "NormalArrow";
    private string _defaultArrowName = "NormalArrow";
    private AmmoData _currArrowData;
    private AmmoData _equippedArrowData;
    private AmmoData _defaultArrowData;
    private float _attackSpeed;


    private float nextFireTime;
    [SerializeField] private bool canShoot = true;
    private void Start()
    {
        Stats = transform.parent.Find("ingameStatManager").GetComponent<ingameStats>();
        _equippedArrowData = AssetDatabase.LoadAssetAtPath<AmmoData>($"Assets/ScriptableObjects/Ammo/{_defaultArrowName}.asset");
        _defaultArrowData = AssetDatabase.LoadAssetAtPath<AmmoData>($"Assets/ScriptableObjects/Ammo/{_defaultArrowName}.asset");
        print(_currArrowData);
    }
    void Update()
    {
        _attackSpeed = Stats._attackSpeed;
        if (Time.time >= nextFireTime)
        {
            if (canShoot)
            {
                if (Input.GetKey(KeyCode.Mouse0) )
                {
                    print(_equippedArrowName);
                    // use current ammo
                    if (AmmoInventory.Instance.useAmmo(_equippedArrowName))
                    {
                        _currArrowData = _equippedArrowData;
                    }
                    // set curr arrow to default if there is no more ammo in the inventory
                    else
                    {
                        _currArrowData = _defaultArrowData;
                    }
                }
                // set curr arrow to default if user doesnt press the mouse
                else _currArrowData = _defaultArrowData;

                nextFireTime = Time.time + 1f / _attackSpeed;

                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 shootDirection = (mousePosition - (Vector2)transform.parent.position).normalized;

                if (Stats._numOfArrows == 1) // Only one projectile, no spread angle
                {
                    //get Ammo prefab from pool
                    GameObject Ammo = AmmoPool.Instance.GetAmmoPrefab();
                    // set the current equipped ammo scriptable object
                    Ammo.GetComponent<AmmoSetup>().Init(_currArrowData);

                    // get the projectile object from Ammo prefab
                    Transform projectile = Ammo.transform.Find("Projectile");

                    // set the position of the projectile to the player position and set rotation to mouse
                    projectile.transform.position = transform.parent.position;
                    projectile.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);

                    Ammo.SetActive(true);

                    // set speed using the stats of the scriptable object
                    projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * Ammo.GetComponent<AmmoSetup>()._ammo.speed;

                }
                else // more than 1 arrow, calculate angle for each arrow
                {
                    // calculate the starting angle
                    float spreadAngle = Mathf.Lerp(MaxSpreadAngle, MinSpreadAngle, (Stats._numOfArrows - 1) / 9f);
                    float angleStep = spreadAngle / (Stats._numOfArrows - 1);
                    float startAngle = -spreadAngle / 2f;

                    for (int i = 0; i < Stats._numOfArrows; i++)
                    {
                        // calculate angle and direction for curr arow
                        float angle = startAngle + angleStep * i;
                        Vector2 projectileDirection = (Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg) * shootDirection).normalized;

                        //get Ammo prefab from pool
                        GameObject Ammo = AmmoPool.Instance.GetAmmoPrefab();
                        // set the current equipped ammo scriptable object
                        Ammo.GetComponent<AmmoSetup>().Init(_currArrowData);

                        // get the projectile object from Ammo prefab
                        Transform projectile = Ammo.transform.Find("Projectile");

                        // set the position of the projectile to the player position
                        projectile.position = transform.parent.position;

                        // set rotation
                        projectile.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);

                        Ammo.SetActive(true);

                        // set velocity
                        projectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * Ammo.GetComponent<AmmoSetup>()._ammo.speed;
                    }
                }
                
            }
        }
    }

    public void setEquippedAmmo(string ammoName)
    {
        _equippedArrowName = ammoName;
        _equippedArrowData = AssetDatabase.LoadAssetAtPath<AmmoData>($"Assets/ScriptableObjects/Ammo/{ammoName}.asset");
    }
}