using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
