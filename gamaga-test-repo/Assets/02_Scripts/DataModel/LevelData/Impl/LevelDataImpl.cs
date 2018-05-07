using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "LevelData", menuName ="Gameplay/Level/LevelData")]
public class LevelDataImpl : ScriptableObject, LevelData 
{
    [SerializeField]
    private List<EnemyCharacterDataImpl> m_enemiesData;

    [SerializeField]
    private LevelEntity m_entityPrefab;

    public List<EnemyCharacterDataImpl> Enemies
    {
        get
        {
            return m_enemiesData;
        }
    }

    public LevelEntity LevelEntity
    {
        get
        {
            return m_entityPrefab;
        }
    }

#if UNITY_EDITOR
    public void ResetValues()
    {
        for(int i=0, count=m_enemiesData.Count; i<count; i++)
        {
            m_enemiesData[i].ResetValues();
        }
        EditorUtility.SetDirty(this);
    }
#endif
}
