using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RealTimeParticle : MonoBehaviour
{
    private ParticleSystem _particle;
    private float _deltaTime;
    private float _timeAtLastFrame;

    void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (_particle == null) return;
        _deltaTime = Time.realtimeSinceStartup - _timeAtLastFrame;
        _timeAtLastFrame = Time.realtimeSinceStartup;
        if (Mathf.Abs(Time.timeScale) < 1e-6)
        {
            _particle.Simulate(_deltaTime, false, false);
            _particle.Play();
        }
    }
}
