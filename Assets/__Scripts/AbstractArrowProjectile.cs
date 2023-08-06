using UnityEngine;


public abstract class AbstractArrowProjectile : MonoBehaviour
{

    public abstract void InitProjectile(Vector2 direction, ArrowData arrowData);
}
