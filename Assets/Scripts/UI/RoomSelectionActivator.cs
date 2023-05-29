using UnityEngine;

public class RoomSelectionActivator : MonoBehaviour
{
    private bool _playerColliding;

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _playerColliding)
        {
            GameManager.Instance.UpdateGameState(GameState.RoomSelection);
            gameObject.SetActive(false);
        }
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
}
