using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class CollectableItem : MonoBehaviour
{
    private const float kDistanceThreshol = 0.4f;

    [SerializeField]
    private string m_destroyAnimationName;

    [SerializeField]
    private Animation m_animation;

    private bool m_destroying;
    private float m_totalTime;

    public bool IsNear(Vector3 position)
    {
        float distance = MathUtils.SqrEuclideanDistance(transform.position, position);
        if(distance <= kDistanceThreshol)
        {
            return true;
        }
        return false;
    }

    public void DestroyCollectable()
    {
        if(m_animation.GetClip(m_destroyAnimationName) != null)
        {
            m_animation.Play(m_destroyAnimationName);
            m_destroying = true;
            m_totalTime = m_animation.GetClip(m_destroyAnimationName).averageDuration;
        }else
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(m_destroying)
        {
            m_totalTime -= TimeManager.Instance.DeltaTime;
            if(m_totalTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
