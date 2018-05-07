using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntity : MonoBehaviour
{
    [SerializeField]
    private List<LevelPoint> m_spawnPoints;

    [SerializeField]
    private LevelPoint m_startingPoint;

    [SerializeField]
    private LevelPoint m_endPoint;

    [SerializeField]
    private Transform m_leftBound;

    [SerializeField]
    private Transform m_rightBound;

    [SerializeField]
    private List<CollectableItem> m_items;
    

    public LevelPoint StartingPoint
    {
        get { return m_startingPoint; }
    }

    public LevelPoint EndingPoint
    {
        get { return m_endPoint; }
    }

    public List<LevelPoint> EnemySpawnPoints
    {
        get { return m_spawnPoints; }
    }

    public Transform LeftBound
    {
        get { return m_leftBound; }
    }

    public Transform RightBound
    {
        get { return m_rightBound; }
    }

    public List<CollectableItem> Items
    {
        get { return m_items; }
    }

    [System.Serializable]
    public class LevelPoint
    {
        private const float kDistanceThreshold = 0.4f;

        [SerializeField]
        private Transform m_spawnPosition;

        public Transform SpawnPosition
        {
            get { return m_spawnPosition; }
        }

        public bool IsNear(Vector3 position)
        {
            float distance = MathUtils.SqrEuclideanDistance(m_spawnPosition.position, position);
            if (distance <= kDistanceThreshold)
            {
                return true;
            }
            return false;
        }
    }
}

