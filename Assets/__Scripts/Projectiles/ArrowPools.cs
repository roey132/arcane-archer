using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ArrowPools : MonoBehaviour
{
    public static ArrowPools Instance;

    [SerializeField] private Dictionary<string, ObjectPool<GameObject>> _pools;
    [SerializeField] private ArrowData _defaultArrow;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _pools = new Dictionary<string, ObjectPool<GameObject>>();
        CreateArrowPool(_defaultArrow);
    }
    private void Start()
    {
        GetArrow(_defaultArrow);
    }

    public void CreateArrowPool(ArrowData arrowData)
    {
        ObjectPool<GameObject> objectPool = new ObjectPool<GameObject>(
            () => { return Instantiate(arrowData.ArrowPrefab, GameManager.Instance.SpellCollector); ; },
            arrowObject => { arrowObject.SetActive(true); },
            arrowObject => { arrowObject.SetActive(false); },
            arrowObject => { Destroy(arrowObject); },
            false,
            100,
            250);
        _pools.Add(arrowData.ArrowName, objectPool);
    }

    public void DestroyArrowPool(string arrowName)
    {
        _pools[arrowName].Dispose();
        _pools.Remove(arrowName);
    }

    public GameObject GetArrow(ArrowData arrowData)
    {
        return _pools[arrowData.ArrowName].Get();
    }

    public void ReleaseArrow(ArrowData arrowData, GameObject arrowObject)
    {
        _pools[arrowData.ArrowName].Release(arrowObject);
    }
}
