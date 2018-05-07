using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class where all the logic related to the MainCharater is proccesed.
/// </summary>
public class PlayerCharacter : Character
{
    public PlayerCharacter(PlayerCharacterData data, GameSession session) : base(data, session)
    {
    }

    protected override void OnUpdate()
    {
        
    }

    public void UpdateAction(EInputAction action)
    {
        switch (action)
        {
            case EInputAction.LeftPressed:
                m_entity.MoveLeft(m_data.MovementSpeed);
                break;
            case EInputAction.RightPressed:
                m_entity.MoveRight(m_data.MovementSpeed);
                break;
            case EInputAction.JumpPressed:
                m_entity.Jump(m_data.JumpForce);
                break;
            case EInputAction.Idle:
                m_entity.Stop();
                break;
        }
    }

    protected override void OnDamage()
    {
        base.OnDamage();
        m_session.NotifyDamageOnMainCharacter(m_currentHealthPoints);
    }
}
