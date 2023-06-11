using UnityEngine;
using UnityEngine.InputSystem;

public class RoomSelectionActivator : MonoBehaviour
{
    private bool _playerColliding;

    private PlayerInputs _playerInputs;
    private InputAction _interact;

    private void Awake()
    {
        _playerInputs = new PlayerInputs();

    }
    private void OnEnable()
    {
        _interact = _playerInputs.Player.Interact;
        _interact.performed += OnInteract;
        _interact.Enable();
    }
    private void OnDisable()
    {
        _interact.performed -= OnInteract;
        _interact?.Disable();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            _playerColliding = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            _playerColliding = false;
        }
    }
    private void OnInteract(InputAction.CallbackContext input)
    {
        if (!_playerColliding) return;

        GameManager.Instance.UpdateGameState(GameState.RoomSelection);
        gameObject.SetActive(false);
    }
}
