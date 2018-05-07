using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_managers;

    private void Start()
    {
        for(int i=0, count=m_managers.Length; i<count; i++)
        {
            GameObject.Instantiate(m_managers[i]);
        }
        GameStateManager.Instance.InitializeStateMachine();
    }

    private void Update()
    {
        Destroy(this.gameObject);
    }

}
