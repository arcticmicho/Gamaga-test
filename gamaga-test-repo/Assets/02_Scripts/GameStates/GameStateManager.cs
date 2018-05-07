using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton with the Base State machine of the game.
/// Controls the different State of the game. Playing, PostGame, Initializating game, etc.
/// Uses GameData class to pass the different info between states.
/// </summary>
public class GameStateManager : MonoSingleton<GameStateManager>
{
    private GenericStateMachine<GameData> m_gameSM;

    public void InitializeStateMachine()
    {
        m_gameSM = new GenericStateMachine<GameData>();
        m_gameSM.Initialize<GameInitState>();
    }

    public void Update()
    {
        if(m_gameSM != null)
        {
            m_gameSM.UpdateStateMachine();
        }
    }

}

/// <summary>
/// Class that contains the different variables used by the Base state machine of the game.
/// No all the state uses all the variables.
/// </summary>
public class GameData
{
    private int m_levelIndex;
    private LevelData m_level;
    private PlayerCharacterData m_characterData;

    private GameSession m_session;
    private bool m_isActiveSession;

    private BlackScreen m_blackScreenInstance;

    public int LevelIndex
    {
        get { return m_levelIndex; }
    }

    public LevelData Level
    {
        get { return m_level; }
    }

    public PlayerCharacterData Character
    {
        get { return m_characterData; }
    }

    public bool IsActiveSession
    {
        get { return m_isActiveSession; }
    }

    public GameSession ActiveSession
    {
        get { return m_session; }
    }

    public BlackScreen BlackScreenInstance
    {
        get { return m_blackScreenInstance; }
    }

    public GameData(int indexLevel, LevelData level, PlayerCharacterData character, BlackScreen blackScreen = null)
    {
        m_levelIndex = indexLevel;
        m_level = level;
        m_characterData = character;
        m_isActiveSession = false;
        m_blackScreenInstance = blackScreen;
    }

    public GameData(GameSession activeSession, int currentLevelIndex, BlackScreen blackScreen = null)
    {
        m_levelIndex = currentLevelIndex;
        m_session = activeSession;
        m_isActiveSession = true;
        m_blackScreenInstance = blackScreen;
    }
}