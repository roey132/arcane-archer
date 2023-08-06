using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaveSpell : AbstractSpell
{
    [SerializeField] private SpellData _spellData;
    [SerializeField] private float _waveDuration;
    [SerializeField] private float _waveSpeed;
    [SerializeField] private GameObject _waveObject;
    [SerializeField] private float _waveSlowSeconds;
    private Transform _player;
    private int _currDirection = 0;

    private float tempTimer = 0f;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _currDirection = 0;
    }
    public override void SpellBehaviour()
    {
        StartCoroutine(ShootWave());
    }
    public void Update()
    {
        tempTimer -= Time.deltaTime;
        print(tempTimer);
        if (tempTimer > 0f) return;
        tempTimer = _spellData.Cooldown;
        SpellBehaviour();
    }
    private IEnumerator ShootWave()
    {
        _currDirection += 1;

        // create wave object
        GameObject wave = Instantiate(_waveObject,GameManager.Instance.SpellCollector);

        // set position of wave to player
        wave.transform.position = _player.transform.position;  

        // set current rotation of the wave
        wave.transform.rotation = Quaternion.Euler(0,0,90 * _currDirection + 90);

        // apply physics
        Rigidbody2D waveRB = wave.GetComponent<Rigidbody2D>();
        waveRB.velocity = GetDirectionVector(90f * _currDirection) * _waveSpeed;

        // set damage
        FireWaveSpellObject objectClass = wave.GetComponent<FireWaveSpellObject>();
        objectClass.SetDamage(_spellData.SpellDamage);

        // handle rest of behaviour
        yield return new WaitForSeconds(_waveDuration);
        waveRB.velocity = GetDirectionVector(90f * _currDirection) * _waveSpeed * 0.1f;
        objectClass.SetToTickDamage();

        yield return new WaitForSeconds(_waveSlowSeconds);
        Destroy(wave);

    }

    private Vector2 GetDirectionVector(float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;

        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);

        return new Vector2(x, y).normalized;
    }
}
