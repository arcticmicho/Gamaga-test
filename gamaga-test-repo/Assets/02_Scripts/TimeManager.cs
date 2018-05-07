using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    private float m_elapsedTime;

    private float m_deltaTime;

    private float m_deltaTimeFactor;

    public float DeltaTime
    {
        get { return m_deltaTime; }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        m_deltaTimeFactor = 1;
        m_elapsedTime = 0;
    }

    private void Update()
    {
        m_elapsedTime += Time.deltaTime * m_deltaTimeFactor;
        m_deltaTime = Time.deltaTime * m_deltaTimeFactor;
    }
}
