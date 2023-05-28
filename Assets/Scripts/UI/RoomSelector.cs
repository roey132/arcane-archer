using UnityEngine;

public class RoomSelector : MonoBehaviour
{
    [SerializeField] private bool _isPlayerColliding = false;
    [SerializeField] private RoomObject _room;
    void Update()
    {
        if (!_isPlayerColliding) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            _room.Activate();
            gameObject.SetActive(false);
        }
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
    public void ActivatePortal(RoomObject room)
    {
        _room = room;
        GetComponent<SpriteRenderer>().sprite = room.PortalSprite;
        gameObject.SetActive(true);
    }
}
