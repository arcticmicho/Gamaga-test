using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile
{
    private int m_projectileDamage;

    private Character m_target;

    private Character m_owner;

    private ProjectileEntity m_prefab;

    private float m_projectileSpeed;

    private ProjectileEntity m_entity;

    private float m_direction;

    private bool m_projectileFinished;

    public bool IsFinished
    {
        get { return m_projectileFinished; }
    }

    public Projectile(Character target, Character owner, ProjectileEntity prefab, int damage, float speed)
    {
        m_target = target;
        m_owner = owner;
        m_prefab = prefab;
        m_projectileDamage = damage;
        m_projectileSpeed = speed;
        m_projectileFinished = false;
    }

    public void CreateEntity()
    {
        if(m_entity != null)
        {
            GameObject.Destroy(m_entity.gameObject);
        }

        m_entity = GameObject.Instantiate<ProjectileEntity>(m_prefab);
        m_entity.transform.SetParent(m_owner.Entity.transform, false);
        m_direction = Mathf.Abs(m_target.Entity.transform.position.x - m_owner.Entity.transform.position.x) / (m_target.Entity.transform.position.x - m_owner.Entity.transform.position.x);

        m_entity.transform.position = m_owner.Entity.transform.position;
    }

    public void UpdateProjectile()
    {
        if(m_entity != null)
        {
            var newPos = m_entity.transform.position + new Vector3(m_direction * m_projectileSpeed * TimeManager.Instance.DeltaTime, 0, 0);
            m_entity.transform.position = newPos;

            if(IsHittingTarget())
            {
                m_target.ApplyDame(m_projectileDamage);
                GameObject.Destroy(m_entity.gameObject);
                m_projectileFinished = true;
            }

            if(!m_entity.IsOnCamera())
            {
                GameObject.Destroy(m_entity.gameObject);
                m_projectileFinished = true;
            }
        }
    }

    private bool IsHittingTarget()
    {
        float distance = MathUtils.EuclideanDistance(m_target.Entity.transform.position, m_entity.transform.position);
        if(distance <= 0.5)
        {
            return true;
        }
        return false;
    }
}
