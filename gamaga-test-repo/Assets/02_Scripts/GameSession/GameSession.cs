using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main class of the game.
/// Correspond to the Logic to play one level. It process all the logic inside a game (Characters, Levels, Enemies, Items) and Check for the finish conditions.
/// This class encapsule all the logic needed to play a level.
/// </summary>
public class GameSession
{
    public enum EGameSessionSate
    {
        PreGame = 0,
        Running = 1,
        PostGame = 2
    }
    private EGameSessionSate m_state;

    private LevelSession m_level;
    private PlayerCharacter m_mainCharacter;

    private LevelEntity m_levelEntity;
    private List<CollectableItem> m_collectibles;

    private List<EnemyCharacter> m_enemies;

    /// <summary>
    /// Flags that represent when the Session can be finished and unloaded.
    /// </summary>
    private bool m_sessionFinished;

    private GameSessionResult m_result;

    private GameInputListener m_inputListener;

    public bool SessionFinished
    {
        get { return m_sessionFinished; }
    }

    public GameSessionResult Result
    {
        get { return m_result; }
    }

    public PlayerCharacter MainCharacter
    {
        get { return m_mainCharacter; }
    }

    public Action<int> OnLifeLost;
    public Action<int> OnScoreChanged;

    public GameSession(PlayerCharacterData character, LevelData level)
    {
        m_mainCharacter = new PlayerCharacter(character, this);
        m_level = new LevelSession(level);
        m_state = EGameSessionSate.PreGame;
        m_result = new GameSessionResult();
    }

    /// <summary>
    /// Load all the assets that the session needs but it doesn't start the Session.
    /// </summary>
    public void LoadSession()
    {
        m_levelEntity = GameObject.Instantiate<LevelEntity>(m_level.Data.LevelEntity);
        m_levelEntity.transform.position = Vector3.zero;

        m_mainCharacter.CreateEntity(m_levelEntity.StartingPoint.SpawnPosition.position);
        m_inputListener = CreateListener();
        m_collectibles = new List<CollectableItem>(m_levelEntity.Items);

        CameraManager.Instance.ResetCameraPosition();

        InstantiateEnemies();
    }

    private void InstantiateEnemies()
    {
        m_enemies = new List<EnemyCharacter>();
        for (int i=0, count = m_level.Data.Enemies.Count; i<count; i++)
        {
            if(i < m_levelEntity.EnemySpawnPoints.Count)
            {
                var newEnemy = new EnemyCharacter(m_level.Data.Enemies[i], this);
                newEnemy.CreateEntity(m_levelEntity.EnemySpawnPoints[i].SpawnPosition.position);
                newEnemy.Entity.SetDirection(CharacterEntity.EDirection.Left);
                m_enemies.Add(newEnemy);
            }
        }
    }

    private GameInputListener CreateListener()
    {
#if  !UNITY_ANDROID
        return new PCInputListener();
#else
        return new MobileInputListener(false);
#endif
    }

    /// <summary>
    /// Start the current session so the user can move the character.
    /// </summary>
    public void StartSession()
    {
        m_state = EGameSessionSate.Running;
        m_inputListener.SetEnableListener(true);
        CameraManager.Instance.FollowTarget(m_mainCharacter.Entity.gameObject, m_levelEntity.LeftBound.position.x, m_levelEntity.RightBound.position.x);
    }

    public void UpdateSession()
    {
        if(m_state == EGameSessionSate.PreGame)
        {

        }else if(m_state == EGameSessionSate.Running)
        {
            EInputAction action = m_inputListener.UpdateListener();
            m_mainCharacter.Update();
            m_mainCharacter.UpdateAction(action);
            for(int i=0, count=m_enemies.Count; i<count; i++)
            {
                m_enemies[i].Update();
            }

            CheckCollectables();
            
            if(CheckWinConditions())
            {
                m_result.SessionWon = true;
                m_state = EGameSessionSate.PostGame;
                m_sessionFinished = true;
                m_mainCharacter.Entity.StopAll();
            }

            if(CheckFailCondition())
            {
                m_result.SessionWon = false;
                m_state = EGameSessionSate.PostGame;
                m_sessionFinished = true;
                m_mainCharacter.Entity.StopAll();
            }
        }
        else if(m_state == EGameSessionSate.PostGame)
        {
            
        }
    }

    private bool CheckFailCondition()
    {
        if(m_mainCharacter.IsDead)
        {
            return true;
        }
        return false;
    }

    private bool CheckWinConditions()
    {
        if(m_levelEntity.EndingPoint.IsNear(m_mainCharacter.Entity.transform.position))
        {
            return true;
        }
        return false;
    }

    private void CheckCollectables()
    {
        for(int i= m_collectibles.Count - 1; i>= 0; i--)
        {
            if(m_collectibles[i].IsNear(m_mainCharacter.Entity.transform.position))
            {
                m_result.Score++;
                m_collectibles[i].DestroyCollectable();
                m_collectibles.RemoveAt(i);

                if(OnScoreChanged != null)
                {
                    OnScoreChanged(m_result.Score);
                }
            }
        }
    }

    public void UnloadSession()
    {
        GameObject.Destroy(m_levelEntity.gameObject);
        GameObject.Destroy(m_mainCharacter.Entity.gameObject);
        for(int i=0, count=m_enemies.Count; i<count; i++)
        {
            GameObject.Destroy(m_enemies[i].Entity.gameObject);
        }
        CameraManager.Instance.ReleaseStrategy();
    }

    public void CloseSession()
    {

    }

    public void NotifyDamageOnMainCharacter(int newHealthPoints)
    {
        if (OnLifeLost != null)
        {
            OnLifeLost(newHealthPoints);
        }
    }

    public class GameSessionResult
    {
        private int m_score;
        private bool m_sessionWon;

        public int Score
        {
            get { return m_score; }
            set { m_score = value; }
        }

        public bool SessionWon
        {
            get { return m_sessionWon; }
            set { m_sessionWon = value; }
        }

        public GameSessionResult()
        {
            m_score = 0;
            m_sessionWon = false;
        }
    }

}
