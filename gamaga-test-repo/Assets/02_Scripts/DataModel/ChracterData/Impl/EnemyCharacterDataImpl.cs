using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCharacterData", menuName = "Gameplay/Character/EnemyCharacterData")]
public class EnemyCharacterDataImpl : CharacterDataImpl, EnemyCharacterData
{
    [SerializeField]
    private int m_projectileDamage = 1;

    [SerializeField]
    private float m_projectileSpeed = 10;

    [SerializeField]
    private float m_fireRate = 3;

    [SerializeField]
    private float m_fireDistance = 1500;

    [SerializeField]
    private ProjectileEntity m_prefab;

    public int ProjectileDamage
    {
        get
        {
            return m_projectileDamage;
        }
    }

    public float ProjectileSpeed
    {
        get
        {
            return m_projectileSpeed;
        }
    }

    public float FireRate
    {
        get
        {
            return m_fireRate;
        }
    }

    public ProjectileEntity ProjectilePrefab
    {
        get
        {
            return m_prefab;
        }
    }

    public float FireDistance
    {
        get
        {
            return m_fireDistance;
        }
    }

#if UNITY_EDITOR
    public override void ResetValues()
    {
        base.ResetValues();
        m_projectileDamage = 1;
        m_projectileSpeed = 10;
        m_fireRate = 3;
        m_fireDistance = 1500;
        EditorUtility.SetDirty(this);
    }
#endif
}
