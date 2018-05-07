using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    private EnemyCharacterData m_enemyData;

    private float m_elapsedTime;

    private List<Projectile> m_projectiles = new List<Projectile>();

    public EnemyCharacter(EnemyCharacterData data, GameSession session) : base(data, session)
    {
        m_enemyData = data;
    }

    protected override void OnUpdate()
    {
        if(IsEnemyAtDistance())
        {
            m_elapsedTime += TimeManager.Instance.DeltaTime;
            float direction = m_session.MainCharacter.Entity.transform.position.x - m_entity.transform.position.x;
            float directionNorm = Mathf.Abs(direction) / direction;
            if (m_elapsedTime >= m_enemyData.FireRate && (directionNorm == (int)m_entity.CurrentDirection))
            {
                m_elapsedTime = 0;
                Projectile newProjectile = new Projectile(m_session.MainCharacter, this, m_enemyData.ProjectilePrefab, m_enemyData.ProjectileDamage, m_enemyData.ProjectileSpeed);
                newProjectile.CreateEntity();
                m_projectiles.Add(newProjectile);
            }
        }

        for(int i=m_projectiles.Count -1; i>=0; i--)
        {
            if(!m_projectiles[i].IsFinished)
            {
                m_projectiles[i].UpdateProjectile();
            }else
            {
                m_projectiles.RemoveAt(i);
            }
        }
    }

    private bool IsEnemyAtDistance()
    {
        float distance = MathUtils.EuclideanDistance(m_entity.transform.position, m_session.MainCharacter.Entity.transform.position);
        if(distance <= m_enemyData.FireDistance)
        {
            return true;
        }
        return false;
    }
}
