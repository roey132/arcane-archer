using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInputs _playerInputs;
    private Vector2 _movementVector = Vector2.zero;

    [SerializeField] private Vector2 movement;
    private void Awake()
    {
        _playerInputs = new PlayerInputs();
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
        print(_movementVector);
        _rb.velocity = _movementVector * IngameStats.Instance.MovementSpeed;
        //if (GameManager.Instance.UiISActive) 
        //{
        //    _rb.velocity = Vector2.zero;
        //    return;
        //}

        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //movement = new Vector2(moveHorizontal, moveVertical);

        //_rb.velocity = movement * IngameStats.Instance.MovementSpeed;
    }
    private void OnMovePerformed(InputAction.CallbackContext inputValue)
    {
        _movementVector = inputValue.ReadValue<Vector2>();
        print(_movementVector);
    }
    private void OnMoveCanceled(InputAction.CallbackContext inputValue)
    {
        _movementVector = Vector2.zero;
        print(_movementVector);
    }
}
