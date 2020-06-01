using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ZombieSpawner
{
    [SerializeField]
    private int m_InitialCount;
    [SerializeField]
    private Zombie m_Prefab;
    [SerializeField]
    private Vector3 m_InitialPos;
    [SerializeField]
    private Transform m_TargetMove;
    [SerializeField]
    private Transform m_CameraTransform;
    private List<Zombie> m_Instantiateds;
    public void Create()
    {
        m_Instantiateds = new List<Zombie>(m_InitialCount);
        for (int i = 0; i < m_InitialCount; i++)
        {
            Zombie zombie = GameObject.Instantiate(m_Prefab, m_InitialPos, Quaternion.identity);
            zombie.Deactivate();
            m_Instantiateds.Add(zombie);
        }
    }

    public int TotalActiveZombie {get{
        int inactiveZombie = 0;
        foreach (var item in m_Instantiateds)
        {
            if(item.State==ZombieState.Inactive)
            {
                inactiveZombie++;
            }            
        }
        return m_Instantiateds.Count-inactiveZombie;
    }
    }

    public Zombie GetOrCreateZombie()
    {
        foreach (var item in m_Instantiateds)
        {
            if(item.State==ZombieState.Inactive)
            {
                return item;
            }
        }
        Zombie zombie = GameObject.Instantiate(m_Prefab, m_InitialPos, Quaternion.identity);
        m_Instantiateds.Add(zombie);
        return zombie;
    }

    public void Spawn()
    {
        var zombie = GetOrCreateZombie();
        float xpos = UnityEngine.Random.Range(-200.0f, 600.0f);
        float speed = UnityEngine.Random.Range(40.0f, 60.0f);
        Vector3 spawnPos = m_TargetMove.position + m_TargetMove.forward * 1000;
        spawnPos.x = xpos;
        zombie.Activate(m_TargetMove, spawnPos, speed);
    }
}