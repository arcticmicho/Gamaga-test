using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : UIView
{
    [SerializeField]
    private Text m_resultTest;

    [SerializeField]
    private Text m_scoreText;

    [SerializeField]
    private Button m_actionButtonPressed;

    [SerializeField]
    private Text m_buttonText;

    private Action m_onActionButtonPressed;

    private void Awake()
    {
        m_actionButtonPressed.onClick.RemoveAllListeners();
        m_actionButtonPressed.onClick.AddListener(OnActionButton);
    }

    public void InitializeResultView(bool won, int score, Action onActionButtonPressed)
    {
        m_resultTest.text = won ? "You Won!!" : "You lost...";
        m_scoreText.text = score.ToString();
        m_onActionButtonPressed = onActionButtonPressed;
        m_buttonText.text = won ? "Next Level" : "Retry";
    }

    private void OnActionButton()
    {
        if(m_onActionButtonPressed != null)
        {
            m_onActionButtonPressed();
        }
        Close();
    }
}
