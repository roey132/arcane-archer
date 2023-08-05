using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowShooter : MonoBehaviour
{
    public static ArrowShooter Instance;

    public GameObject ProjectilePrefab;
    public float MaxSpreadAngle;
    public float MinSpreadAngle;

    private ArrowData _currArrowData;
    private ArrowData _equippedArrowData;
    [SerializeField] private ArrowData _defaultArrowData;
    private float _attackSpeed;
    private float _numOfArrows;
    private IngameStats _stats;
    private Transform _player;
    private Transform _bow;

    private PlayerInputs _playerInputs;
    private InputAction _fire;
    private bool _isUsingAmmo;

    private float nextFireTime;
    [SerializeField] private SpriteRenderer _equippedAmmoSprite;
    [SerializeField] private bool _canShoot = true;

    public static event Action ShootArrow; 

    void Awake()
    {
        _playerInputs = new PlayerInputs();
        if (Instance == null)
        {
            Instance = this;
        }

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _bow = GameObject.FindGameObjectWithTag("Player").transform.Find("Bow").transform;
        GameManager.OnGameStateChange += CanShoot;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= CanShoot;
    }
    private void OnEnable()
    {
        BowAnimations.ShootAction += Shoot;

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
        BowAnimations.ShootAction -= Shoot;

        if (_equippedAmmoSprite != null)
        {
            _equippedAmmoSprite.gameObject.SetActive(false);
        }

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

        if (IngameStats.Instance.EquippedArrow != null) 
        {
            _equippedAmmoSprite.gameObject.SetActive(true);
            _equippedAmmoSprite.sprite = IngameStats.Instance.EquippedArrow.Sprite;
        }
        else _equippedAmmoSprite.gameObject.SetActive(false);;

        _attackSpeed = _stats.AttackSpeed;
        _numOfArrows = _stats.NumOfArrows;


        if (Time.time <= nextFireTime) return;

        if (!_canShoot) return;

        nextFireTime = Time.time + 1f / _attackSpeed;
        ShootArrow?.Invoke();
    }

    public void Shoot()
    {
        _currArrowData = _defaultArrowData;
        if (_isUsingAmmo && IngameStats.Instance.EquippedArrow != null)
        {
            _currArrowData = IngameStats.Instance.EquippedArrow;
            ArrowInventory.Instance.UseArrow(_currArrowData);
        }

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
        //get Arrow prefab from pool
        GameObject Arrow = ArrowPool.Instance.GetAmmoPrefab();
        // set the current equipped arrow scriptable object
        Arrow.GetComponent<ArrowSetup>().Init(_currArrowData);

        // get the projectile object from Arrow prefab
        Transform projectile = Arrow.transform.Find("Projectile");

        // set the position of the projectile to the player position and set rotation to mouse
        projectile.transform.position = _bow.position;
        projectile.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);

        Arrow.SetActive(true);

        // set speed using the stats of the scriptable object
        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * Arrow.GetComponent<ArrowSetup>().Arrow.Speed;
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

            //get Arrow prefab from pool
            GameObject Arrow = ArrowPool.Instance.GetAmmoPrefab();
            // set the current equipped arrow scriptable object
            Arrow.GetComponent<ArrowSetup>().Init(_currArrowData);

            // get the projectile object from Arrow prefab
            Transform projectile = Arrow.transform.Find("Projectile");

            // set the position of the projectile to the player position
            projectile.position = _bow.position;

            // set rotation
            projectile.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);

            Arrow.SetActive(true);

            // set velocity
            projectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * Arrow.GetComponent<ArrowSetup>().Arrow.Speed;
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