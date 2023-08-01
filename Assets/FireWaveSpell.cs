using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaveSpell : AbstractSpell
{
    [SerializeField] private SpellData spellData;
    [SerializeField] private float waveDuration;
    [SerializeField] private float waveSpeed;
    [SerializeField] private GameObject waveObject;
    private Transform Player;
    private int currDirection = 0;

    private float tempTimer = 0f;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        currDirection = 0;
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
        tempTimer = 0.3f;
        SpellBehaviour();
    }
    private IEnumerator ShootWave()
    {
        // create wave object
        GameObject wave = Instantiate(waveObject,GameManager.Instance.SpellCollector);

        // set position of wave to player
        wave.transform.position = Player.transform.position;  

        // set current rotation of the wave
        wave.transform.rotation = Quaternion.Euler(0,0,90*currDirection + 90);

        // apply physics
        Rigidbody2D waveRB = wave.GetComponent<Rigidbody2D>();
        waveRB.velocity = GetDirectionVector(90f * currDirection) * waveSpeed;

        // change next direction
        currDirection += 1;
        if (currDirection == 4) currDirection = 0;

        // handle rest of behaviour
        yield return new WaitForSeconds(waveDuration);
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
