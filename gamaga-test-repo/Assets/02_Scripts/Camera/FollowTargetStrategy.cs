using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Strategy to move the Camera based on a Object
/// </summary>
public class FollowTargetStrategy : CameraStrategy
{
    private GameObject m_target;
    private float m_leftBorder;
    private float m_rightBorder;


    public FollowTargetStrategy(GameObject target, float leftBorder, float rightBorder)
    {
        m_target = target;
        m_leftBorder = leftBorder;
        m_rightBorder = rightBorder;
    }

    public Vector3 CalculateCameraPosition()
    {
        float positionX = m_target.transform.position.x;
        if(positionX >= m_leftBorder && positionX <= m_rightBorder)
        {
            return new Vector3(positionX, CameraManager.Instance.Main.transform.position.y, CameraManager.Instance.Main.transform.position.z);
        }else
        {
            return CameraManager.Instance.Main.transform.position;
        }
    }
}
