using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    [SerializeField]
    private GameResources m_gameResources;

    public GameResources GameResources
    {
        get { return m_gameResources; }
    }

    public LevelData GetLevelByIndex(int i)
    {
        if(m_gameResources.Levels.Levels.Count > i && i >= 0)
        {
            return m_gameResources.Levels.Levels[i];
        }
        return null;
    }
    
}
