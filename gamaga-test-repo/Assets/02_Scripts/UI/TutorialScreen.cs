using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScreen : UIView
{
    [SerializeField]
    private Text m_instructionMove;

    [SerializeField]
    private Text m_instructionJump;

    [SerializeField]
    private Button m_continueButton;

    private Action m_onContinuePressed;

    private void Awake()
    {
        m_continueButton.onClick.RemoveAllListeners();
        m_continueButton.onClick.AddListener(OnContinuePressed);
    }

    public void InitializeTutorialView(Action onContinuePressed)
    {
        m_instructionMove.text = GetInstructionMove();
        m_instructionJump.text = GetInstructionJump();

        m_onContinuePressed = onContinuePressed;
    }

    private void OnContinuePressed()
    {
        if(m_onContinuePressed != null)
        {
            m_onContinuePressed();
        }
        Close();
    }

    private string GetInstructionJump()
    {
#if !UNITY_ANDROID
        return "Use the Space bar to jump.";
#else
        return "Tap to jump.";
#endif
    }

    private string GetInstructionMove()
    {
#if !UNITY_ANDROID
        return "Use the Left and Right arrow to move.";
#else
        return "Hold on the Right or Left side of the screen to move.";
#endif
    }
}
