using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInputs _playerInputs;
    private Vector2 _movementVector = Vector2.zero;
    public static event Action OnTeleportStart;
    public static event Action OnTeleportEnd;


    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashCooldown;
    private float _dashTimer;

    [SerializeField] LayerMask _objectsLayerMask;
    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        GameManager.OnGameStateChange += CanMove;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= CanMove;
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _playerInputs.Enable();
        _playerInputs.Player.Move.performed += OnMovePerformed;
        _playerInputs.Player.Move.canceled += OnMoveCanceled;
        _playerInputs.Player.Dash.performed += OnDashPerformed;
    }
    private void OnDisable()
    {
        _playerInputs.Disable();
        _playerInputs.Player.Move.performed -= OnMovePerformed;
        _playerInputs.Player.Move.canceled -= OnMoveCanceled;
        _playerInputs.Player.Dash.performed -= OnDashPerformed;
    }

    private void Update()
    {
        _dashTimer -= Time.deltaTime;
        _dashTimer = Mathf.Clamp(_dashTimer,0,_dashCooldown);
    }
    private void FixedUpdate()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right,_dashDistance, _objectsLayerMask);
        if (hit.collider != null)
        {
            print(hit.collider);
            if (hit.collider.CompareTag("Wall"))
            {
                print("ray hits wall");
                float hitDistance = hit.distance;
                print(hitDistance);
                if (hitDistance < _dashDistance)
                {
                    print("close to wall");
                }
            }
        }
        Debug.DrawRay(transform.position, Vector2.right * _dashDistance,Color.red);
        _rb.velocity = _movementVector * IngameStats.Instance.MovementSpeed;
    }
    private void OnMovePerformed(InputAction.CallbackContext inputValue)
    {
        _movementVector = inputValue.ReadValue<Vector2>();
    }
    private void OnMoveCanceled(InputAction.CallbackContext inputValue)
    {
        _movementVector = Vector2.zero;
    }
    private void OnDashPerformed(InputAction.CallbackContext inputValue)
    {
        Dash();
    }
    private void Dash()
    {
        if (_dashTimer > 0) return;
        OnTeleportStart?.Invoke();
        _dashTimer = _dashCooldown;
        float dashDistance = _dashDistance;
        Vector2 dashDirection;

        dashDirection = Vector2.right;
        if (_movementVector != Vector2.zero)
        {
            dashDirection = _movementVector;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, _dashDistance, _objectsLayerMask);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                float hitDistance = hit.distance;
                print(hitDistance);
                if (hitDistance < _dashDistance)
                {
                    dashDistance = hitDistance;
                }
            }
        }

        _rb.position += dashDistance * dashDirection;
        StartCoroutine(DelayedTeleportEnd());
    }
    public void CanMove(GameState state)
    {
        if (state == GameState.BuffSelection)
        {
            _playerInputs.Disable();
            return;
        }
        _playerInputs.Enable();
        return;
    }
    public IEnumerator DelayedTeleportEnd()
    {
        yield return new WaitForSeconds(0.1f) ;
        OnTeleportEnd?.Invoke();
    }
}
