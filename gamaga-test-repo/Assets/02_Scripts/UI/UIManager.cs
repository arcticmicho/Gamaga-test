using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIManager to show differents view based on the UIViewResources. Each time a view is requested, a copy of the view is created and returned.
/// Each consumer of UIManager is responsible of what to do the view generated.
/// Works like a Queue but it doesn't use the Queue structure. The last requested view is showed first to the user. Except the cases where the prioritie flag is set.
/// </summary>
public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private UIViewResource m_views;

    [SerializeField]
    private RectTransform m_canvasTransform;

    [SerializeField]
    private RectTransform m_priorityCanvasTransform;

    Dictionary<Type, UIView> m_cachedViewTypes;

    private List<UIView> m_activeViews = new List<UIView>();

    public void Initialize()
    {
        m_cachedViewTypes = new Dictionary<Type, UIView>();
        for (int i = 0, count = m_views.Views.Count; i < count; i++)
        {
            if (!m_cachedViewTypes.ContainsKey(m_views.Views[i].GetType()))
            {
                m_cachedViewTypes.Add(m_views.Views[i].GetType(), m_views.Views[i]);
            }
        }
    }

    /// <summary>
    /// Method to request a View from the UIManager.
    /// It will return a Copy of the selected view  and put it on the top of the canvas.
    /// If priority is set, it will put the View in another parent of the canvas with more Priority.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="priority"></param>
    /// <returns></returns>
    public T RequestView<T>(bool priority = false) where T: UIView
    {
        if(m_cachedViewTypes.ContainsKey(typeof(T)))
        {
            T newView = GetViewInstance<T>(m_cachedViewTypes[typeof(T)] as T, priority);
            m_activeViews.Add(newView);
            newView.Open();
            return newView;
        }
        return null;
    }

    /// <summary>
    /// Remove the Present view of the list and Destroy it. This method should be called after closing the view.
    /// </summary>
    /// <param name="view"></param>
    public void CloseView(UIView view)
    {
        if(m_activeViews.Contains(view))
        {
            m_activeViews.Remove(view);
            GameObject.Destroy(view.gameObject);
        }
    }

    private T GetViewInstance<T>(T uIView, bool priotiy) where T : UIView
    {
        T newView = GameObject.Instantiate<T>(uIView);
        newView.transform.SetParent(priotiy ? m_priorityCanvasTransform : m_canvasTransform, false);
        newView.transform.localPosition = Vector3.zero;
        newView.transform.SetAsLastSibling();
        return newView;
    }
}
