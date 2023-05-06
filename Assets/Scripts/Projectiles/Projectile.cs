using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage;
    void Start()
    {
        print($"player transform is {transform.parent.parent.parent}");
        Collider2D playerCollider = transform.parent.parent.parent.gameObject.GetComponent<Collider2D>();
        Collider2D thisCollider = transform.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(thisCollider, playerCollider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.tag);
        if (!collision.CompareTag("Player") && !collision.CompareTag("Projectile"))
        {
            if (collision.CompareTag("Enemy")){
                collision.GetComponent<Enemy>().hit(damage);
            }
            Debug.Log(collision.tag);
            transform.parent.gameObject.SetActive(false);
        }
    }
    public void setDamage(int damage)
    {
        this.damage = damage;
    }
}
