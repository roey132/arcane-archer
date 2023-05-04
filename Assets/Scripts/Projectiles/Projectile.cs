using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage = 1;
    void Start()
    {
        Collider2D playerCollider = transform.parent.gameObject.GetComponent<Collider2D>();
        Collider2D thisCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(thisCollider, playerCollider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Projectile"))
        {
            if (collision.CompareTag("Enemy")){
                collision.GetComponent<Enemy>().hit(damage);
            }
            Debug.Log(collision.tag);
            Destroy(gameObject, 0f);
        }
    }
}
