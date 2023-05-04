using UnityEngine;

public class FireOrbSpell : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed;
    public float radius;

    private int damage = 5;

    private Vector3 rotationAxis = Vector3.forward;
    void Start()
    {
        player = transform.parent;
    }
    void Update()
    {
        transform.RotateAround(player.position, rotationAxis, rotationSpeed * Time.deltaTime);
        Vector3 offset = new Vector3(radius, 0, 0);
        transform.position = player.position + (transform.position - player.position).normalized * radius;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("orb hit enemy");
            collision.GetComponent<Enemy>().hit(damage);
        }
        
    }
}

