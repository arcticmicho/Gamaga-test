using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Scriptable Object that hold the information for the Main Character. Here we can add logic to select different Characters.
/// </summary>
[CreateAssetMenu(fileName ="CharacterResource", menuName ="Gameplay/Character/CharacterResource")]
public class CharacterResource : ScriptableObject
{
    [SerializeField]
    private PlayerCharacterDataImpl m_mainCharacter;
    

    public PlayerCharacterDataImpl MainCharacter
    {
        get
        {
            return m_mainCharacter;
        }
    }

    internal void ResetValues()
    {
#if UNITY_EDITOR
        m_mainCharacter.ResetValues();
        EditorUtility.SetDirty(this);
#endif
    }
}
