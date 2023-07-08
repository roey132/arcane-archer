using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomSelector : MonoBehaviour
{
    [SerializeField] private bool _isPlayerColliding = false;
    [SerializeField] private RoomObject _room;
    [SerializeField] private TextMeshPro _difficultyText;

    private int _currDifficulty;
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

        
        _room.Activate(_currDifficulty);

        gameObject.SetActive(false);
    }

    public void InitPortal(RoomObject room)
    {
        _room = room;
        GetComponent<SpriteRenderer>().sprite = room.PortalSprite;
        SetDifficulty();
        _difficultyText.text = _currDifficulty.ToString();
        gameObject.SetActive(true);
    }

    public void SetDifficulty()
    {
        int currFloor = GameManager.Instance.CurrentFloor;

        int minDifficulty = currFloor - 2;

        // set min difficulty to not go below 1
        minDifficulty = Mathf.Clamp(minDifficulty, 1, 100);

        int maxDifficulty = currFloor + 3;

        _currDifficulty = Random.Range(minDifficulty, maxDifficulty);
    }
}
