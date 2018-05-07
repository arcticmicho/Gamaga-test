using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUDView : UIView
{
    [SerializeField]
    private LifeWidget m_lifePrefab;

    [SerializeField]
    private Transform m_lifeWidgetParent;

    [SerializeField]
    private Text m_scoreText;

    private List<LifeWidget> m_currentLifes = new List<LifeWidget>();

    public void InitializeGameHUD(int totalLifes)
    {
        for(int i=0; i< totalLifes; i++)
        {
            LifeWidget newWidget = GameObject.Instantiate<LifeWidget>(m_lifePrefab);
            newWidget.SetLife(true);
            newWidget.transform.SetParent(m_lifeWidgetParent, false);
            m_currentLifes.Add(newWidget);
        }
    }

    public void SetScoreText(int value)
    {
        m_scoreText.text = value.ToString();
    }

    public void SetCurrentLifes(int lifes)
    {
        for(int i=0, count = m_currentLifes.Count; i<count; i++)
        {
            m_currentLifes[i].SetLife(i < lifes ? true : false);
        }
    }

}
