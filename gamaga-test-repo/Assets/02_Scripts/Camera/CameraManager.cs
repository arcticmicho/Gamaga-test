using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple Camera Manager manage the position.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraManager : MonoSingleton<CameraManager>
{
    /// <summary>
    /// Cache of the main camera, since Camera.main call FindObjectWithTag everytime.
    /// </summary>
    private Camera m_main;

    private CameraStrategy m_currentStrategy;

    public Camera Main
    {
        get
        {
            return m_main;
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        m_main = GetComponent<Camera>();
        if(m_main != Camera.main)
        {
            Debug.LogError("Camera from CameraManager is not the main camera");
            m_main = Camera.main;
        }
    }

    private void Update()
    {
        if(m_currentStrategy != null)
        {
            m_main.transform.position = m_currentStrategy.CalculateCameraPosition();
        }
    }

    public void FollowTarget(GameObject target)
    {
        FollowTarget(target, float.MinValue, float.MaxValue);
    }

    public void FollowTarget(GameObject target, float leftBorder, float rightBorder)
    {
        m_currentStrategy = new FollowTargetStrategy(target, leftBorder, rightBorder);
    }

    internal void ResetCameraPosition()
    {
        m_main.transform.position = new Vector3(0, 0, m_main.transform.position.z);
    }

    internal void ReleaseStrategy()
    {
        m_currentStrategy = null;
    }
}
