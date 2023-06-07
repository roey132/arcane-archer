using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;
    private PlayerInputs _playerInputs;
    private bool _isWalking;

    [SerializeField] private ParticleSystem _walkParticles;

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

        _isWalking = (horizontalAxis != 0 || verticalAxis != 0);
        print(_isWalking);
        HandleWalkParticles();

        if (horizontalAxis != 0)
        {
            _spriteRenderer.flipX = horizontalAxis < 0;
        }

        var state = _isWalking ? Animator.StringToHash("NoctisWalk") : Animator.StringToHash("IdleNoctis");

        //_animator.SetBool("isWalking", horizontalAxis != 0 || verticalAxis != 0);
        _animator.CrossFade(state, 0);

    }
    private void HandleWalkParticles()
    {
        if (_isWalking && !_walkParticles.isPlaying)
        {
            _walkParticles.Play();
            return;
        }
        if (!_isWalking)
        {
            _walkParticles.Stop();
        }
    }
}
