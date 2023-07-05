using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField] private float _spinsPerSecond;

    void Update()
    {
        float rotationAngle = 360f * _spinsPerSecond * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationAngle);
    }

}
