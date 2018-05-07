using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface to reprsent the Input Controller for the GameSession. With this we can make different implementation based on the platform
/// </summary>
public interface GameInputListener
{
    EInputAction UpdateListener();
    void SetEnableListener(bool enable);
}

public enum EInputAction
{
    LeftPressed = 0,
    RightPressed = 1,
    JumpPressed = 2,
    Idle = 3
}
