using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "LevelResources", menuName = "Gameplay/Level/LevelResources")]
public class LevelResource : ScriptableObject
{

    [SerializeField]
    private List<LevelDataImpl> m_levels;

    public List<LevelDataImpl> Levels
    {
        get { return m_levels; }
    }

#if UNITY_EDITOR
    public void ResetValues()
    {
        for(int i=0, count= m_levels.Count; i<count; i++)
        {
            m_levels[i].ResetValues();
        }
        EditorUtility.SetDirty(this);
    }
#endif
}
