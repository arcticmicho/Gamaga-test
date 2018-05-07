using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UITransitioner : MonoBehaviour
{
    public abstract void PlayOpenAnimation(Action<bool> onAnimationFinished);
    public abstract void PlayCloseAnimation(Action<bool> onAnimationFinished);
}
