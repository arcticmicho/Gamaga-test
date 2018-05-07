using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Input Listener for PC
/// </summary>
public class PCInputListener : GameInputListener
{
    private bool m_enabled;

    public void SetEnableListener(bool enable)
    {
        m_enabled = enable;
    }

    public EInputAction UpdateListener()
    {
        if(m_enabled)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                return EInputAction.JumpPressed;
            }else if (Input.GetKey(KeyCode.LeftArrow))
            {
                return EInputAction.LeftPressed;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                return EInputAction.RightPressed;
            }            
            else
            {
                return EInputAction.Idle;
            }
        }else
        {
            return EInputAction.Idle;
        }
        
    }
}
