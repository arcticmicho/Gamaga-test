using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransition<Data> where Data : class
{
    private Type m_stateType;

    private Data m_data;

    public Type StateType
    {
        get { return m_stateType; }
    }

    public Data StateData
    {
        get { return m_data; }
    }

    public bool IsNull
    {
        get { return m_stateType == null; }
    }

    public StateTransition(Type stateType, Data data)
    {
        m_stateType = stateType;
        m_data = data;
    }

    public static StateTransition<Data> NullTransition = new StateTransition<Data>(null, null);
	
    public static StateTransition<Data> Transition<State>(Data data = null)
    {
        return new StateTransition<Data>(typeof(State), data);
    }

}
