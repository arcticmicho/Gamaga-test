using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State used to select the first level to appear. Normally this state should have a UI to show what level is selected but for now we can always start at level 1.
/// TODO: Remove Splash screen and add UI to select level.
/// </summary>
public class GameLevelSelectionState : GenericState<GameData>
{
    private const float kWaitingTime = 2f;
    private LevelData m_levelSelected;
    private bool m_goToSelectedLevel;
    private bool m_finishedSplash;

    private BlackScreen m_blackScreen;
    private GamagaTest m_gamagaView;

    private float m_elapsedTime;

    public override StateTransition<GameData> EvaluateTransition()
    {
        if(m_goToSelectedLevel)
        {
            m_gamagaView.Close();
            return StateTransition<GameData>.Transition<GameSessionState>(new GameData(0, ResourceManager.Instance.GetLevelByIndex(0), ResourceManager.Instance.GameResources.CharacterResources.MainCharacter, m_blackScreen));
        }
        return StateTransition<GameData>.NullTransition;
    }

    protected override void OnExitState()
    {

    }

    protected override void OnOpenState()
    {
        m_levelSelected = ResourceManager.Instance.GetLevelByIndex(0);
        m_gamagaView = UIManager.Instance.RequestView<GamagaTest>();
        m_elapsedTime = 0;
        m_goToSelectedLevel = false;
        m_finishedSplash = false;
    }

    private void OnBlackScreenOpened()
    {
        m_goToSelectedLevel = true;
    }

    protected override void OnUpdateState()
    {
        m_elapsedTime += TimeManager.Instance.DeltaTime;
        if(m_elapsedTime >= kWaitingTime && m_finishedSplash == false)
        {
            m_finishedSplash = true;
            m_blackScreen = UIManager.Instance.RequestView<BlackScreen>(true);
            m_blackScreen.OnViewOpened += OnBlackScreenOpened;
        }
    }
}
