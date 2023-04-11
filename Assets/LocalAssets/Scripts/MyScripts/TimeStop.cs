using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    [SerializeField] ParticleSystem particle1;
    [SerializeField] ParticleSystem particle2;
    [SerializeField] ParticleSystem particle3;
    [SerializeField] ParticleSystem particle4;
    [SerializeField] ParticleSystem particle5;

    float _timeScale;
    bool _isStop;
    bool _isStopP1;
    bool _isStopP2;
    bool _isStopP3;
    bool _isStopP4;
    bool _isStopP5;

    private void OnEnable()
    {
        _timeScale = Time.timeScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TheWorld();
        }

        ParticleStop1();
        ParticleStop2();
        ParticleStop3();
        ParticleStop4();
        ParticleStop5();
    }

    void TheWorld()
    {
        if (!_isStop)
        {
            Time.timeScale = 0;
            _isStop = true;
        }
        else
        {
            Time.timeScale = _timeScale;
            _isStop = false;
        }
    }

    void ParticleStop1()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) && particle1)
            ParticleStop(ref _isStopP1, particle1);
    }

    void ParticleStop2()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2) && particle2)
            ParticleStop(ref _isStopP2, particle2);
    }

    void ParticleStop3()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3) && particle3)
            ParticleStop(ref _isStopP3, particle3);
    }

    void ParticleStop4()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4) && particle4)
            ParticleStop(ref _isStopP4, particle4);
    }

    void ParticleStop5()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5) && particle5)
            ParticleStop(ref _isStopP5, particle5);
    }

    void ParticleStop(ref bool flag, ParticleSystem target)
    {
        if (!flag)
        {
            target.Pause();
            flag = true;
        }
        else
        {
            target.Play();
            flag = false;
        }
    }
}
