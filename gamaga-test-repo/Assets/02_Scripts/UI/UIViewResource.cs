using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIViewResources", menuName = "UI/View List")]
public class UIViewResource : ScriptableObject
{
    [SerializeField]
    private List<UIView> m_views;

    public List<UIView> Views
    {
        get { return m_views; }
    }
}
