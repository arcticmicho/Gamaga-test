using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptables Object that holds all the Parameter that are being used in the game. From the Characters (Enemy and Player) to the Levels;
/// </summary>
[CreateAssetMenu(fileName = "GameResources", menuName = "Gameplay/Gameresources")]
public class GameResources : ScriptableObject
{
    [SerializeField]
    private CharacterResource m_characterResources;

    [SerializeField]
    private LevelResource m_levels;

    public CharacterResource CharacterResources
    {
        get { return m_characterResources; }
    }

    public LevelResource Levels
    {
        get { return m_levels; }
    }

    public void ResetValue()
    {
#if UNITY_EDITOR
        m_characterResources.ResetValues();
        m_levels.ResetValues();
#endif
    }

}
