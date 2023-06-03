using UnityEngine;

public class BowPosition : MonoBehaviour
{
    private Transform _player;
    private float radius = 1.5f;


    private void Start()
    {
        _player = transform.parent;
    }
    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)_player.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = (Vector2)_player.position + direction * radius;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }
    
}
