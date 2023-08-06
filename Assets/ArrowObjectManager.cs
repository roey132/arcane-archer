using System;
using UnityEngine;

public class ArrowObjectManager : MonoBehaviour
{
    [SerializeField] private ArrowData _arrowData;
    [SerializeField] private AbstractArrowProjectile _arrowProjectile;

    private Action OnArrowDisable;
    private Action<ArrowData> OnArrowEnable;

    private void OnDisable()
    {
        OnArrowDisable?.Invoke();
    }

    public void InitProjectile(Vector2 direction,Vector3 startPosition, Quaternion startRotation)
    {
        _arrowProjectile.InitProjectile(direction,_arrowData);
        _arrowProjectile.transform.position = startPosition;
        _arrowProjectile.transform.rotation = startRotation;
    }
}
