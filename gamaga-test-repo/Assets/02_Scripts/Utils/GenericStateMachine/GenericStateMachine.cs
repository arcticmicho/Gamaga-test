using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic State Machine that receive a class type to be used as data to pass between States. The transition can't be forced. It must be done by each state.
/// It's very useful for Gampley programming such as AI algorithms, since the behavior of each state is data driven.
/// The states used by the State Machine are cached so the can be re-used (like a pool). For that reason each state must clean himself when transitioning.
/// This is done to avoid too much allocation memory since the State are created using activator.
/// </summary>
/// <typeparam name="Data"></typeparam>
public class GenericStateMachine<Data> where Data : class
{
    private Dictionary<Type, GenericState<Data>> m_statesCache;

    private GenericState<Data> m_currentState;

    public void Initialize<State>(Data data = null) where State: GenericState<Data>
    {
        m_statesCache = new Dictionary<Type, GenericState<Data>>();
        ChangeState(StateTransition<Data>.Transition<State>(data));
    }

    public void UpdateStateMachine()
    {
        if(m_currentState != null)
        {
            var transition = m_currentState.EvaluateTransition();
            if(!transition.IsNull)
            {
                ChangeState(transition);
            }
            m_currentState.UpdateState();
        }
    }

    private void ChangeState(StateTransition<Data> stateTransition)
    {
        if(!stateTransition.IsNull)
        {
            if(m_currentState != null)
            {
                m_currentState.ExitState();
            }
            m_currentState = GetCachedState(stateTransition.StateType);
            m_currentState.OpenState(stateTransition.StateData);
        }
    }

    private GenericState<Data> GetCachedState(Type stateType)
    {
        if(m_statesCache.ContainsKey(stateType))
        {
            return m_statesCache[stateType];
        }else
        {
            GenericState<Data>  newState = (GenericState<Data>)Activator.CreateInstance(stateType);
            m_statesCache.Add(stateType, newState);
            return newState;
        }

    }
}
