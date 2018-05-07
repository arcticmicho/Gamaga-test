using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSession
{
    private LevelData m_data;

    public LevelData Data
    {
        get { return m_data; }
    }	

    public LevelSession(LevelData level)
    {
        m_data = level;
    }
}
