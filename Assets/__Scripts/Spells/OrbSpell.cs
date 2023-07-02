using JetBrains.Annotations;
using UnityEngine;

public class OrbSpell : MonoBehaviour
{
    private Transform _rotateAround;
    private float _rotationSpeed;
    private float _radius;
    private bool _isActive;
    private float _damage;

    private Vector3 rotationAxis = Vector3.forward;

    void Update()
    {
        if (!_isActive) return;

        transform.RotateAround(_rotateAround.position, rotationAxis, _rotationSpeed * Time.deltaTime);
        Vector3 offset = new Vector3(_radius, 0, 0);
        transform.position = _rotateAround.position + (transform.position - _rotateAround.position).normalized * _radius;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("orb hit enemy");
            collision.GetComponent<EnemyStats>().Hit(_damage);
        }
    }
    public void InitOrb(Transform playerTransform, float rotationSpeed, float radius, float damage)
    {
        _isActive = true;
        _rotateAround = playerTransform;
        _rotationSpeed = rotationSpeed;
        _radius = radius;
        _damage = damage;
    }
}

