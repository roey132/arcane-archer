using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArrowPools : MonoBehaviour
{

    [SerializeField] private ArrowData _defaultArrow;
    private void Start()
    {
        InvokeRepeating("GetArrowData", 0,2);
    }
    // Start is called before the first frame update
    private void GetArrowData()
    {
        ArrowPools.Instance.GetArrow(_defaultArrow);
    }
}
