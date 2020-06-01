using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ZombieSpawner m_ZombieSpawner;
    private float m_ZombieSpawnTimerSec;
    private const float ZOMBIE_SPAWN_INTERVAL_SEC = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        m_ZombieSpawner.Create();
    }

    void Update()
    {
        m_ZombieSpawnTimerSec+=Time.deltaTime;
        if(m_ZombieSpawnTimerSec>=ZOMBIE_SPAWN_INTERVAL_SEC)
        {
            m_ZombieSpawnTimerSec=0;
            if(m_ZombieSpawner.TotalActiveZombie<2)
            {
                m_ZombieSpawner.Spawn();
            }
        }
    }
}
