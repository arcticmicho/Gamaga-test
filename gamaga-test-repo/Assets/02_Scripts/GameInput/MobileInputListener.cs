using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implementation for Mobiles.
/// TODO: Need tweaks. Hold and Tap is not intuitive.
/// </summary>
public class MobileInputListener : GameInputListener
{
    private EInputAction m_currentAction;

    public MobileInputListener(bool setEnable)
    {
        m_currentAction = EInputAction.Idle;
        if (setEnable)
        {
            RegisterListeners();
        }
    }

    public void SetEnableListener(bool enable)
    {
        if(enable)
        {
            RegisterListeners();
        }else
        {
            UnregisterListeners();
        }
    }

    public EInputAction UpdateListener()
    {
        if (m_currentAction == EInputAction.JumpPressed)
        {
            m_currentAction = EInputAction.Idle;
            return EInputAction.JumpPressed;
        }
        return m_currentAction;
    }

    public void RegisterListeners()
    {
        InputManager.Instance.OnHoldEvent += OnHold;
        InputManager.Instance.OnTapEvent += OnTap;
    }

    public void UnregisterListeners()
    {
        InputManager.Instance.OnHoldEvent -= OnHold;
        InputManager.Instance.OnTapEvent -= OnTap;
    }

    private void OnTap(Vector3 position)
    {
        m_currentAction = EInputAction.JumpPressed;
    }

    private void OnHold(HoldStatus status, Vector3 position)
    {
        if(status == HoldStatus.Begin)
        {
            Vector3 viewportPosition = CameraManager.Instance.Main.ScreenToViewportPoint(position);
            if(viewportPosition.x >= 0.5)
            {
                m_currentAction = EInputAction.RightPressed;
            }
            else
            {
                m_currentAction = EInputAction.LeftPressed;
            }            
        }else
        {
            m_currentAction = EInputAction.Idle;
        }
    }
}
