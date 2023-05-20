using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSelector : MonoBehaviour
{
    
    [SerializeField] private float _duration;
    private float _durationTimer;

    [SerializeField] private float _slowTimeScale;

    [SerializeField] private float _slowMotionCooldown;
    private float _slowMotionTimer;

    [SerializeField] private bool _slowMotionActive;

    [SerializeField] private AnimationCurve _slowMotionCurve;

    private float _currentAnimationCurve;

    void Start()
    {
        _durationTimer = _duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _durationTimer > 0 && _slowMotionTimer <= 0)
        {
            _slowMotionActive = true;
            _currentAnimationCurve = 0f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && _durationTimer > 0 && _slowMotionTimer <= 0 && _slowMotionActive)
        {
            SlowMotion();
            _durationTimer -= Time.unscaledDeltaTime;
        }
        else 
        {
            if (_slowMotionActive == true)
            {
                _durationTimer = _duration;
                _slowMotionActive = false;
                _slowMotionTimer = _slowMotionCooldown;
            }
            StopSlowMotion();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Time.timeScale = 1f;
        }

        if (_slowMotionTimer > 0)
        {
            _slowMotionTimer -= Time.unscaledDeltaTime;
        }
    }
    private void SlowMotion()
    {

        _currentAnimationCurve = Mathf.MoveTowards(_currentAnimationCurve, 1f, Time.unscaledDeltaTime / _duration);
        print(Mathf.Lerp(_slowTimeScale, 1f, Time.timeScale));
        Time.timeScale = Mathf.Lerp(_slowTimeScale, 1f,_slowMotionCurve.Evaluate(_currentAnimationCurve));
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    private void StopSlowMotion()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
