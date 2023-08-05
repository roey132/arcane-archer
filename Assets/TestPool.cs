using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TestPool : MonoBehaviour
{
    [SerializeField] private GameObject _testPrefab;

    [SerializeField] private ObjectPool<GameObject> objectPool;

    private GameObject test;
    [SerializeField] private bool destroyPool;
    private void Start()
    {
        objectPool = new ObjectPool<GameObject>(
            () => { return Instantiate(_testPrefab, GameManager.Instance.SpellCollector); },
            testObject => { testObject.SetActive(true); },
            testObject => { testObject.SetActive(false); },
            testObject => { Destroy(testObject); },
            false,
            50,
            200);
    }

    private void Update()
    {
        if (!destroyPool) return;
        objectPool.Release(test);
        objectPool.Dispose();
        destroyPool = false;
    }

    private void OnEnable()
    {
        test = objectPool.Get();
    }
    private void OnDisable()
    {
    }
}
