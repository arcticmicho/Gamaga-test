using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEntity : MonoBehaviour
{

    public bool IsOnCamera()
    {
        var pos = CameraManager.Instance.Main.WorldToViewportPoint(transform.position);
        if(pos.x >= 0 && pos.x <= 1)
        {
            return true;
        }
        return false;
    }

}
