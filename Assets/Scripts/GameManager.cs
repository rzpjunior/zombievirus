using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ZombieSpawner m_ZombieSpawner;
    [SerializeField]
    private UIManager ui;
    [SerializeField]
    private Camera m_mainCamera;
    [SerializeField]
    private MachineGun machineGun;
    private float m_ZombieSpawnTimerSec;
    private const float ZOMBIE_SPAWN_INTERVAL_SEC = 0.5f;
    private bool isShoot;
    private Vector3 m_PosTolerance = new Vector3(0.5f, 0.5f, 0);

    void Start()
    {
        m_ZombieSpawner.Create();
        ui.SetGame(this);
        machineGun.SetUI(ui);
        machineGun.UpdateBullet();
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
        if(isShoot)
        {
            isShoot = false;
            int bulletCount = machineGun.ValidateShoot();
            if(bulletCount>0)
            {
                var ray = m_mainCamera.ViewportPointToRay(m_PosTolerance);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    Zombie zombie = hit.transform.GetComponent<Zombie>();
                    if(zombie!=null)
                    {
                        zombie.TakeDamage();
                    }
                }
            }
        }
    }

    public void Shoot()
    {
        isShoot = true;
    }
}
