using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericState<Data> where Data : class
{
    protected Data m_data;

    public void OpenState(Data data)
    {
        m_data = data;
        OnOpenState();
    }

    /// <summary>
    /// Abstract method to define when the state must transition to another state. A null transition can be returned so the State Machine won't change of state
    /// </summary>
    /// <returns></returns>
    public abstract StateTransition<Data> EvaluateTransition();

    public void UpdateState()
    {
        OnUpdateState();
    }

    public void ExitState()
    {
        OnExitState();
        m_data = null;
    }

    protected abstract void OnOpenState();
    protected abstract void OnUpdateState();
    protected abstract void OnExitState();
	
}
