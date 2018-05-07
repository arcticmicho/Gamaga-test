using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State that show the result of the  current game session.
/// Based on the result, it will offer go to the Next level or retry the current one.
/// </summary>
public class GamePostSessionState : GenericState<GameData>
{
    private GameSession m_activeSession;
    private ResultView m_resultView;

    private BlackScreen m_blackScreen;

    private bool m_retry;
    private bool m_nextLevel;

    public override StateTransition<GameData> EvaluateTransition()
    {
        if(m_retry)
        {
            return StateTransition<GameData>.Transition<GameSessionState>(new GameData(m_data.LevelIndex, ResourceManager.Instance.GetLevelByIndex(m_data.LevelIndex), ResourceManager.Instance.GameResources.CharacterResources.MainCharacter, m_blackScreen));
        }
        if(m_nextLevel)
        {
            var nextLevel = ResourceManager.Instance.GetLevelByIndex(m_data.LevelIndex + 1);
            if(nextLevel != null)
            {
                return StateTransition<GameData>.Transition<GameSessionState>(new GameData(m_data.LevelIndex + 1, nextLevel, ResourceManager.Instance.GameResources.CharacterResources.MainCharacter, m_blackScreen));
            }else
            {
                return StateTransition<GameData>.Transition<GameSessionState>(new GameData(0, ResourceManager.Instance.GetLevelByIndex(0), ResourceManager.Instance.GameResources.CharacterResources.MainCharacter, m_blackScreen));
            }
            
        }
        return StateTransition<GameData>.NullTransition;
    }

    protected override void OnExitState()
    {
        m_activeSession.UnloadSession();
    }

    protected override void OnOpenState()
    {
        m_activeSession = m_data.ActiveSession;
        m_resultView = UIManager.Instance.RequestView<ResultView>();
        m_resultView.InitializeResultView(m_activeSession.Result.SessionWon, m_activeSession.Result.Score, OnActionButtonPressed);
        m_retry = false;
        m_nextLevel = false;
    }

    private void OnActionButtonPressed()
    {
        m_blackScreen = UIManager.Instance.RequestView<BlackScreen>(true);
        m_blackScreen.OnViewOpened += () =>
        {
            if (m_activeSession.Result.SessionWon)
            {
                m_nextLevel = true;
            }
            else
            {
                m_retry = true;
            }
        };
        
    }

    protected override void OnUpdateState()
    {
        
    }
}
