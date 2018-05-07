using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface to define a Strategy to move the camera.
/// </summary>
public interface CameraStrategy
{
    Vector3 CalculateCameraPosition();
}
