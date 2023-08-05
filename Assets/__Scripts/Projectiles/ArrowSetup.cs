using UnityEngine;

public class ArrowSetup: MonoBehaviour
{
    public ArrowData Arrow;
    public SpriteRenderer _spriteRenderer;
    public ArrowBehaviour _arrowBehaviour;

    public void Init(ArrowData arrow)
    {
        Arrow = arrow;
        _arrowBehaviour.gameObject.SetActive(true);
        _arrowBehaviour.InitProjectile(arrow);
    }
}
