using UnityEditor;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public static ProjectileShooter Instance;

    public GameObject ProjectilePrefab;
    public float MaxSpreadAngle;
    public float MinSpreadAngle;

    private string _equippedArrowName = "NormalArrow";
    private string _defaultArrowName = "NormalArrow";
    private AmmoData _currArrowData;
    private AmmoData _equippedArrowData;
    private AmmoData _defaultArrowData;
    private float _attackSpeed;
    private float _numOfArrows;
    private IngameStats _stats;


    private float nextFireTime;
    [SerializeField] SpriteRenderer equippedAmmoSprite;
    [SerializeField] private bool canShoot = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        _stats = IngameStats.Instance;
        _equippedArrowData = AssetDatabase.LoadAssetAtPath<AmmoData>($"Assets/ScriptableObjects/Ammo/{_defaultArrowName}.asset");
        _defaultArrowData = AssetDatabase.LoadAssetAtPath<AmmoData>($"Assets/ScriptableObjects/Ammo/{_defaultArrowName}.asset");
    }
    void Update()
    {
        if (GameManager.Instance.UiISActive) return;

        _attackSpeed = _stats.AttackSpeed;
        _numOfArrows = _stats.NumOfArrows;

        if (Time.time >= nextFireTime)
        {
            if (canShoot)
            {
                if (Input.GetKey(KeyCode.Mouse0) )
                {
                    // use current ammo
                    if (AmmoInventory.Instance.useAmmo(_equippedArrowData))
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

                if (_numOfArrows == 1) // Only one projectile, no spread angle
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
                    projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * Ammo.GetComponent<AmmoSetup>()._ammo.Speed;

                }
                else // more than 1 arrow, calculate angle for each arrow
                {
                    // calculate the starting angle
                    float spreadAngle = Mathf.Lerp(MaxSpreadAngle, MinSpreadAngle, (_numOfArrows - 1) / 9f);
                    float angleStep = spreadAngle / (_numOfArrows - 1);
                    float startAngle = -spreadAngle / 2f;

                    for (int i = 0; i < _numOfArrows; i++)
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
                        projectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * Ammo.GetComponent<AmmoSetup>()._ammo.Speed;
                    }
                }
                
            }
        }
    }

    public void setEquippedAmmo(AmmoData ammoData)
    {
        _equippedArrowName = ammoData.name;
        _equippedArrowData = ammoData;
        equippedAmmoSprite.gameObject.SetActive(true);
        equippedAmmoSprite.sprite = ammoData.Sprite;

    }
}