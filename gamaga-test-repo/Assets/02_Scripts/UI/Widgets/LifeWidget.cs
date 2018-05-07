using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeWidget : MonoBehaviour
{
    [SerializeField]
    private Image m_heartImage;

    [SerializeField]
    private Sprite m_fullSprite;

    [SerializeField]
    private Sprite m_emptySprite;

    public void SetLife(bool full)
    {
        m_heartImage.sprite = full ? m_fullSprite : m_emptySprite;
    }
}
