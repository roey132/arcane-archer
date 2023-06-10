using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MeleeEnemyController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _followMaxRadius;
    [SerializeField] private float _followMinRadius;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _backwardMovementSpeed;
    [SerializeField] private float _distance;
    [SerializeField] private Vector2 _dirToPlayer;
    [SerializeField] public bool InMaxRadiusRange { get; private set; }


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector2.Distance(transform.position, _player.position);
        _dirToPlayer = transform.position - _player.position;
        InMaxRadiusRange = _distance < _followMaxRadius;

        MoveTowardsPlayer();
        MoveAwayFromPlayer();
    }
    private void MoveTowardsPlayer()
    {
        if (InMaxRadiusRange) return;
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _movementSpeed * Time.deltaTime);
    }
    private void MoveAwayFromPlayer()
    {
        if (!InMaxRadiusRange) return;
        if (_distance == _followMinRadius) return;
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)_player.position + _dirToPlayer.normalized * _followMinRadius, _backwardMovementSpeed * Time.deltaTime);
    }
    
}

