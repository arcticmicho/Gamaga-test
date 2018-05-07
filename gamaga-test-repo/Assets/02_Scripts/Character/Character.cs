using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller (from the logic point of view) of the MainCharacter. 
/// Is the Controller part of the MVC pattern for the character.
/// Contains the Entity (View) and hold the Data and the temporary values of the data used in the game
/// </summary>
public abstract class Character
{
    protected int m_currentHealthPoints;
    protected float m_movementSpeed;

    protected bool m_isDead;

    protected CharacterData m_data;

    protected CharacterEntity m_entity;

    protected GameSession m_session;

    public CharacterEntity Entity
    {
        get { return m_entity; }
    }

    public bool IsDead
    {
        get { return m_isDead; }
    }

    public Character(CharacterData data, GameSession session)
    {
        m_data = data;
        m_currentHealthPoints = data.InitialHP;
        m_movementSpeed = data.MovementSpeed;
        m_session = session;
    }

    public CharacterData Data
    {
        get { return m_data; }
    }

    public void Update()
    {
        if(m_entity != null)
        {
            m_entity.UpdateEntity();
        }
        OnUpdate();
    }

    public void CreateEntity(Vector3 worldPosition)
    {
        if(m_entity != null)
        {
            GameObject.Destroy(m_entity);
        }

        m_entity = GameObject.Instantiate<CharacterEntity>(m_data.Entity);
        m_entity.transform.position = worldPosition;
    }

    public void ApplyDame(int damage)
    {
        m_currentHealthPoints = Mathf.Max(0, m_currentHealthPoints - damage);

        if(m_currentHealthPoints <= 0)
        {
            m_isDead = true;
        }

        OnDamage();
    }

    protected abstract void OnUpdate();

    protected virtual void OnDamage()
    {

    }
}
