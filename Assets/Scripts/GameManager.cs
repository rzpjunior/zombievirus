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
    [SerializeField]
    private int m_PlayerInitialHealth;
    [SerializeField]
    private int m_DamagePerZombiePerAttack;
    private int m_PlayerHealth;

    private const float ZOMBIE_ATTACK_INTERVAL_SEC = 1.0f;
    private float m_ZombieAttackTimer;
    private GameState m_GameState=GameState.Stop;
    void Start()
    {
        m_PlayerHealth = m_PlayerInitialHealth;
        m_ZombieSpawner.Create();
        ui.SetGame(this);
        machineGun.SetUI(ui);
        machineGun.UpdateBullet();
        ui.UpdatePlayerHealthLabel(m_PlayerHealth.ToString());
        m_GameState= GameState.Playing;
    }

    void Update()
    {
        if(m_GameState==GameState.Stop)
            return;
        SpawnZombie();
        PlayerShoot();
        PlayerTakeDamage();
    }

    private void SpawnZombie()
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

    private void PlayerShoot()
    {
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

    private void PlayerTakeDamage()
    {
        if(GameData.ZOMBIE_ATTACKER_COUNT>0)
        {
            m_ZombieAttackTimer+=Time.deltaTime;
            if(m_ZombieAttackTimer>=ZOMBIE_ATTACK_INTERVAL_SEC)
            {
                m_ZombieAttackTimer=0;
            }
            if(m_ZombieAttackTimer==0)
            {
                if(m_PlayerHealth>0)
                {
                    m_PlayerHealth-=(GameData.ZOMBIE_ATTACKER_COUNT*m_DamagePerZombiePerAttack);
                }
            }
            if(m_PlayerHealth<=0)
            {
                m_PlayerHealth=0;
            }
            ui.UpdatePlayerHealthLabel(m_PlayerHealth.ToString());
            if(m_PlayerHealth==0)
            {
                StopGame();
            }
        }
    }
    private void StopGame()
    {
        ui.HideAll();
        machineGun.gameObject.SetActive(false);
        m_ZombieSpawner.DeactivateAll();
        m_GameState=GameState.Stop;
    }
    public void Shoot()
    {
        isShoot = true;
    }
}
