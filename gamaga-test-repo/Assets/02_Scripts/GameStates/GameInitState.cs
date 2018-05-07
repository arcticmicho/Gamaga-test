using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitState : GenericState<GameData>
{

    private bool m_finishLoading;

    public override StateTransition<GameData> EvaluateTransition()
    {
        if(m_finishLoading)
        {
            return StateTransition<GameData>.Transition<GameLevelSelectionState>();
        }
        return StateTransition<GameData>.NullTransition;
    }

    protected override void OnExitState()
    {
        
    }

    protected override void OnOpenState()
    {
        m_finishLoading = false;
        GameStateManager.Instance.StartCoroutine(LoadingResources());
    }

    protected override void OnUpdateState()
    {
        
    }

    private IEnumerator LoadingResources()
    {
        yield return SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);

        UIManager.Instance.Initialize();

        m_finishLoading = true;
    }
}
