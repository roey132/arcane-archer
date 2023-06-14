using UnityEngine;
using UnityEngine.InputSystem;

public class RoomSelector : MonoBehaviour
{
    [SerializeField] private bool _isPlayerColliding = false;
    [SerializeField] private RoomObject _room;

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
        if (collision.CompareTag("Player")){
            _isPlayerColliding = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerColliding = false;
        }
    }
    public void OnInteract(InputAction.CallbackContext input)
    {
        if (!_isPlayerColliding) return;
        _room.Activate();
        gameObject.SetActive(false);
    }

    public void InitPortal(RoomObject room)
    {
        _room = room;
        GetComponent<SpriteRenderer>().sprite = room.PortalSprite;
        gameObject.SetActive(true);
    }
}