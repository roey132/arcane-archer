using System;
using UnityEngine;

public class BowAnimations : MonoBehaviour
{
    private Animator _animator;

    private float animationDuration;

    public static event Action ShootAction;

    private float time;
    private float counter = 0f;
    private bool startCount = false;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        animationDuration = 10f/60f;
        time = 0f;
    }

    private void Update()
    {
        time += Time.deltaTime; 
        if (1f / IngameStats.Instance.AttackSpeed > animationDuration) return;

        float speed = animationDuration / (1 / IngameStats.Instance.AttackSpeed);

        _animator.speed = speed * 1.5f;
        print($"set speed to {speed}");
    }

    private void OnEnable()
    {
        ArrowShooter.ShootArrow += ShootAnimation;
    }
    private void OnDisable()
    {
        ArrowShooter.ShootArrow -= ShootAnimation;
    }
    public void ShootAnimation()
    {

        print("this function runs");
        _animator.CrossFade(Animator.StringToHash("BowIdle"), 0);
        _animator.CrossFade(Animator.StringToHash("BowShoot"), 0);
        if (!startCount)
        {
            startCount = true;
            time = 0f;
        }
    }
    public void OnShootAnimationEnd() 
    {
        ShootAction?.Invoke();
        counter++;
    }

}
