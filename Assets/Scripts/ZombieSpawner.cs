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
    private Transform m_TargetMove;
    [SerializeField]
    private Transform m_CameraTransform;
    [SerializeField]
    private float m_ZombieRandomWalkSpeedMin;
    [SerializeField]
    private float m_ZombieRandomWalkSpeedMax;
    private List<Zombie> m_Instantiateds;
    private System.Random m_RandomXPos = new System.Random();
    private Vector3 m_VectorZero = new Vector3(0,0,0);
    private int m_PreviousXPos;
    public void Create()
    {
        m_Instantiateds = new List<Zombie>(m_InitialCount);
        for (int i = 0; i < m_InitialCount; i++)
        {
            Zombie zombie = GameObject
            .Instantiate(m_Prefab, m_VectorZero, Quaternion.identity);
            zombie.name = "zombie_"+i;
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
        Zombie zombie = GameObject.Instantiate(m_Prefab, m_VectorZero, Quaternion.identity);
        zombie.name = "zombie_"+m_Instantiateds.Count;
        m_Instantiateds.Add(zombie);
        return zombie;
    }

    public void Spawn()
    {
        var zombie = GetOrCreateZombie();
        //camera horizon size, create random number between -200 to 600 with step of 100
        int randomXPos = m_RandomXPos.Next(-2, 6)*100;
        while (randomXPos==m_PreviousXPos)
        {
             randomXPos = m_RandomXPos.Next(-2, 6)*100;  
        }
        m_PreviousXPos = randomXPos;
        float xpos = randomXPos;
        float speed = UnityEngine.Random.Range(m_ZombieRandomWalkSpeedMin, m_ZombieRandomWalkSpeedMax);
        Vector3 spawnPos = m_TargetMove.position + m_TargetMove.forward * 1000;
        spawnPos.x = xpos;
        zombie.Activate(m_TargetMove, spawnPos, speed);
    }

    public void DeactivateAll()
    {
        foreach (var item in m_Instantiateds)
        {
            if(item.State!=ZombieState.Inactive)
            {
                item.Deactivate();
            }
        }
    }
}