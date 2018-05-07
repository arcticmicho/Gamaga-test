using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State that manage the GameSession for one particular level. 
/// It checks when the Session ends and transition to the ResultState.
/// In charge of show the UI for the GameSession.
/// </summary>
public class GameSessionState : GenericState<GameData>
{
    private GameSession m_currentSession;
    private GameHUDView m_hud;
    private TutorialScreen m_tutorialScreen;

    public override StateTransition<GameData> EvaluateTransition()
    {
        if(m_currentSession.SessionFinished)
        {
            return StateTransition<GameData>.Transition<GamePostSessionState>(new GameData(m_currentSession, m_data.LevelIndex));
        }
        return StateTransition<GameData>.NullTransition;
    }

    protected override void OnExitState()
    {
        m_currentSession.OnLifeLost -= OnLifeLost;
        m_currentSession.OnScoreChanged -= OnScoreChanged;
        m_hud.Close();
    }

    protected override void OnOpenState()
    {
        if(!m_data.IsActiveSession)
        {
            m_currentSession = new GameSession(m_data.Character, m_data.Level);
            m_currentSession.LoadSession();
            
            //if the level is the first one, we show the tutorial screen and when that screen is closes, we start the session.
            if(m_data.LevelIndex == 0)
            {
                m_tutorialScreen = UIManager.Instance.RequestView<TutorialScreen>();
                m_tutorialScreen.InitializeTutorialView(OnContinueButtonPressed);
            }
            else
            {
                m_currentSession.StartSession();
            }
            
        }else
        {
            m_currentSession = m_data.ActiveSession;
        }

        if(m_data.BlackScreenInstance != null)
        {
            m_data.BlackScreenInstance.Close();
        }

        m_hud = UIManager.Instance.RequestView<GameHUDView>();
        m_hud.InitializeGameHUD(m_data.Character.InitialHP);
        m_hud.SetScoreText(0);
        m_currentSession.OnLifeLost += OnLifeLost;
        m_currentSession.OnScoreChanged += OnScoreChanged;
        
    }

    private void OnContinueButtonPressed()
    {
        m_currentSession.StartSession();
    }

    public void OnLifeLost(int lifes)
    {
        if (m_hud != null)
        {
            m_hud.SetCurrentLifes(lifes);
        }
    }

    private void OnScoreChanged(int score)
    {
        if(m_hud != null)
        {
            m_hud.SetScoreText(score);
        }
    }

    protected override void OnUpdateState()
    {
        if(m_currentSession != null)
        {
            m_currentSession.UpdateSession();
        }
    }
}
