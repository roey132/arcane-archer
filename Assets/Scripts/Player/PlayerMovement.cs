using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInputs _playerInputs;
    private Vector2 _movementVector = Vector2.zero;
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
    }
    private void OnDisable()
    {
        _playerInputs.Disable();
        _playerInputs.Player.Move.performed -= OnMovePerformed;
        _playerInputs.Player.Move.canceled -= OnMoveCanceled;
    }
    private void FixedUpdate()
    {
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
}
