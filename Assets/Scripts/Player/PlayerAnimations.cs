using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;
    private PlayerInputs _playerInputs;

    void Start()
    {
        _playerInputs = new PlayerInputs();

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        
    }
    void Update()
    {
        if (GameManager.Instance.UiISActive) 
        {
            _animator.CrossFade(Animator.StringToHash("IdleNoctis"),0);
            return;
        }
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        bool isWalking = (horizontalAxis != 0 || verticalAxis != 0);

        if (horizontalAxis > 0)
        {
            _spriteRenderer.flipX = true;
        }
        if (horizontalAxis < 0)
        {
            _spriteRenderer.flipX = false;
        }

        var state = isWalking ? Animator.StringToHash("NoctisWalk") : Animator.StringToHash("IdleNoctis");

        //_animator.SetBool("isWalking", horizontalAxis != 0 || verticalAxis != 0);
        _animator.CrossFade(state, 0);
    }
}
