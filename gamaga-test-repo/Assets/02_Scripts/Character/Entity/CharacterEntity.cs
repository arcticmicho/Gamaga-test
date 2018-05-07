using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class CharacterEntity : MonoBehaviour
{
    public enum EDirection
    {
        Left = -1,
        Right = 1
    }

    public enum EEntityState
    {
        Jumping = 0,
        RecievingDamage = 1,
        None = 2
    }

    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private Rigidbody2D m_rigidBody;

    [SerializeField]
    private Collider2D m_collider;

    private EEntityState m_state;
    private EDirection m_currentDirection;

    public EDirection CurrentDirection
    {
        get { return m_currentDirection; }
    }

    public void SetDirection(EDirection direction)
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (int)direction, transform.localScale.y, transform.localScale.z);
        m_currentDirection = direction;
    }

    public void MoveLeft(float speed)
    {
        m_rigidBody.velocity = new Vector2(speed * -1,  m_rigidBody.velocity.y);
        SetDirection(EDirection.Left);
        if(m_state != EEntityState.Jumping)
        {
            PlayAnimation("Run");
        }        
    }

    public void MoveRight(float speed)
    {
        m_rigidBody.velocity = new Vector2(speed, m_rigidBody.velocity.y);
        SetDirection(EDirection.Right);
        if (m_state != EEntityState.Jumping)
        {
            PlayAnimation("Run");
        }
    }

    public void Jump(float speed)
    {
        if(m_state != EEntityState.Jumping)
        {
            m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, speed);
            m_state = EEntityState.Jumping;
            PlayAnimation("Jump");
        }        
    }

    public void Stop()
    {
        m_rigidBody.velocity = new Vector2(0, m_rigidBody.velocity.y);
        if (m_state != EEntityState.Jumping)
        {
            PlayAnimation("Idle");
        }
    }

    public void StopAll()
    {
        m_rigidBody.velocity = Vector2.zero;
    }
    
    public void UpdateEntity()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (m_state == EEntityState.Jumping)
        {
            m_state = EEntityState.None;
            m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, 0);

            PlayAnimation(m_rigidBody.velocity.x != 0 ? "Run" : "Idle");
        }
    }

    private void PlayAnimation(string trigger)
    {
        if(m_animator != null)
        {
            m_animator.SetTrigger(trigger);
        }
    }

    private ContactPoint2D GetHigherContact(ContactPoint2D[] contacts)
    {
        ContactPoint2D contact = contacts[0]; 
        for(int i=0, count=contacts.Length; i<count; i++)
        {
            if(contact.point.y < contacts[i].point.y)
            {
                contact = contacts[i];
            }
        }
        return contact;
    }
}
