using UnityEngine;

public class OrbPositions : MonoBehaviour
{
    public GameObject orbPrefab;
    public int numOrbs;
    public float radius;
    public Transform centerTransform;
    private Transform _player;

    private GameObject[] orbs;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        createOrbs();
    }
    private void destroyOrbs()
    {
        for (int i = 0;i < numOrbs;i++) 
        {
            Destroy(orbs[i]);
        }
    }
    private void createOrbs()
    {
        orbs = new GameObject[numOrbs];

        for (int i = 0; i < numOrbs; i++)
        {
            float angle = i * Mathf.PI * 2f / numOrbs;
            Vector3 spawnPos = _player.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
            orbs[i] = Instantiate(orbPrefab, spawnPos, Quaternion.identity, _player);
        }
    }
    public void addOrb()
    {
        destroyOrbs();
        numOrbs++;
        createOrbs();
    }
}
