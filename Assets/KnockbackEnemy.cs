using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    public UnityAction startKnockback;
    public UnityAction endKnockback;
    void Start()
    {
        _rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector3 forceDirection, float force, float delay)
    {
        if (!this.enabled) return;
        startKnockback?.Invoke();

        _rb.AddForce(forceDirection.normalized * force, ForceMode2D.Impulse);
        StartCoroutine(knockbackDelay(delay));

        endKnockback?.Invoke();
    }
    private IEnumerator knockbackDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _rb.velocity = Vector2.zero;
    }
}
