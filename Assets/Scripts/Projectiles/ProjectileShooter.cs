using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileShooter : MonoBehaviour
{
    public static ProjectileShooter Instance;

    public GameObject ProjectilePrefab;
    public float MaxSpreadAngle;
    public float MinSpreadAngle;

    private AmmoData _currArrowData;
    private AmmoData _equippedArrowData;
    [SerializeField] private AmmoData _defaultArrowData;
    private float _attackSpeed;
    private float _numOfArrows;
    private IngameStats _stats;
    private Transform _player;

    private PlayerInputs _playerInputs;
    private InputAction _fire;
    private bool _isUsingAmmo;

    private float nextFireTime;
    [SerializeField] private SpriteRenderer _equippedAmmoSprite;
    [SerializeField] private bool _canShoot = true;

    void Awake()
    {
        _playerInputs = new PlayerInputs();
        if (Instance == null)
        {
            Instance = this;
        }

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        GameManager.OnGameStateChange += CanShoot;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= CanShoot;
    }
    private void OnEnable()
    {
        _equippedAmmoSprite.gameObject.SetActive(true);
        _isUsingAmmo = false;

        // handle input actions
        _playerInputs.Enable();
        _fire = _playerInputs.Player.Fire;
        _fire.performed += OnFirePerformed;
        _fire.Enable();
    }


    private void OnDisable()
    {
        _equippedAmmoSprite.gameObject.SetActive(false);

        // handle input actions
        _fire.performed -= OnFirePerformed;
        _fire.Disable();
    }
    private void Start()
    {
        _stats = IngameStats.Instance;
        _equippedArrowData = _defaultArrowData;
    }
    void Update()
    {
        if (GameManager.Instance.UiISActive) return;

        if (IngameStats.Instance.EquippedAmmo != null) 
        {
            _equippedAmmoSprite.gameObject.SetActive(true);
            _equippedAmmoSprite.sprite = IngameStats.Instance.EquippedAmmo.Sprite;
        }
        else _equippedAmmoSprite.gameObject.SetActive(false);;

        _attackSpeed = _stats.AttackSpeed;
        _numOfArrows = _stats.NumOfArrows;

        if (Time.time <= nextFireTime) return;

        if (!_canShoot) return;

        _currArrowData = _defaultArrowData;
        if (_isUsingAmmo && IngameStats.Instance.EquippedAmmo != null)
        {
            _currArrowData = IngameStats.Instance.EquippedAmmo;
            AmmoInventory.Instance.useAmmo(_currArrowData);
        }

        nextFireTime = Time.time + 1f / _attackSpeed;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - (Vector2)_player.position).normalized;

        if (_numOfArrows == 1) 
        {
            ShootOneArrow(shootDirection);
            return;
        }
        ShootMultipleArrows(shootDirection); 
        return;
    }
    

    public void ShootOneArrow(Vector2 shootDirection)
    {
        //get Ammo prefab from pool
        GameObject Ammo = AmmoPool.Instance.GetAmmoPrefab();
        // set the current equipped ammo scriptable object
        Ammo.GetComponent<AmmoSetup>().Init(_currArrowData);

        // get the projectile object from Ammo prefab
        Transform projectile = Ammo.transform.Find("Projectile");

        // set the position of the projectile to the player position and set rotation to mouse
        projectile.transform.position = _player.position;
        projectile.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);

        Ammo.SetActive(true);

        // set speed using the stats of the scriptable object
        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * Ammo.GetComponent<AmmoSetup>()._ammo.Speed;
    }
    private void ShootMultipleArrows(Vector2 shootDirection)
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
            projectile.position = _player.position;

            // set rotation
            projectile.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);

            Ammo.SetActive(true);

            // set velocity
            projectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * Ammo.GetComponent<AmmoSetup>()._ammo.Speed;
        }
    }
    public void CanShoot(GameState state)
    {
        gameObject.SetActive(state == GameState.InCombat);
    }
    public void OnFirePerformed(InputAction.CallbackContext input) 
    {
        _isUsingAmmo = 0 < input.ReadValue<float>();
    }
}