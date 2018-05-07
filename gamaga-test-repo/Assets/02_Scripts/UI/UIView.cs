using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField]
    protected UITransitioner m_transitioner;

    public Action OnViewOpened;
    public Action OnViewClosed;

    public enum EUIViewState
    {
        Opening = 0,
        Opened = 1,
        Closing = 2,
        Closed = 3
    }

    protected EUIViewState m_state;

    public void Open()
    {
        gameObject.SetActive(true);
        m_state = EUIViewState.Opening;
        OnOpen();
        if(m_transitioner != null)
        {
            m_transitioner.PlayOpenAnimation(Opened);
        }else
        {
            Opened(false);
        }
    }

    private void Opened(bool transitionCanceled)
    {
        if(OnViewOpened != null)
        {
            OnViewOpened();
        }
        OnOpened();
    }

    private void Closed(bool transitionCanceled)
    {
        if(OnViewClosed != null)
        {
            OnViewClosed();
        }
        OnClosed();
        NotifyViewClosed();
    }

    public void Close()
    {
        OnClose();
        if(m_transitioner != null)
        {
            m_transitioner.PlayCloseAnimation(Closed);
        }else
        {
            Closed(false);
        }
    }

    private void NotifyViewClosed()
    {
        UIManager.Instance.CloseView(this);
    }

    protected virtual void OnClose() { }

    protected virtual void OnOpen() { }

    protected virtual void OnOpened() { }

    protected virtual void OnClosed() { }
}
