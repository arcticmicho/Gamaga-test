using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class CharacterDataImpl : ScriptableObject, CharacterData
{
    [SerializeField]
    private int m_initialHP = 3;

    [SerializeField]
    private float m_movementSpeed = 5;

    [SerializeField]
    private float m_jumpForce = 8;

    [SerializeField]
    protected CharacterEntity m_entity;

    public int InitialHP
    {
        get
        {
            return m_initialHP;
        }
    }

    public float MovementSpeed
    {
        get
        {
            return m_movementSpeed;
        }
    }

    public CharacterEntity Entity
    {
        get
        {
            return m_entity;
        }
    }

    public float JumpForce
    {
        get
        {
            return m_jumpForce;
        }
    }

#if UNITY_EDITOR
    public virtual void ResetValues()
    {
        m_initialHP = 3;
        m_movementSpeed = 5;
        m_jumpForce = 8;
        EditorUtility.SetDirty(this);
    }
#endif
}
