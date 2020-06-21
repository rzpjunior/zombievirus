using System.Collections;
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
    [SerializeField]
    private int m_CountDownMaxSec;
    private readonly WaitForSeconds m_CountdownWaitSec = new WaitForSeconds(1.0f);
    private int m_PlayerHealth;
    private int m_CountDownTimerSec;
    private const float ZOMBIE_ATTACK_INTERVAL_SEC = 1.0f;
    private float m_ZombieAttackTimer;
    private GameState m_GameState=GameState.Stop;
    private const int SCORE_PER_ZOMBIE = 100;
    private int m_PlayerScore = 0;
    private int m_TotalKilledZombie = 0;
    void Start()
    {
        m_PlayerHealth = m_PlayerInitialHealth;
        m_ZombieSpawner.Create();
        ui.SetGame(this);
        machineGun.SetUI(ui);
        machineGun.UpdateBullet();
        ui.UpdatePlayerHealthLabel(m_PlayerHealth.ToString());
        m_GameState= GameState.Playing;
        m_CountDownTimerSec = m_CountDownMaxSec;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {

        while(m_GameState==GameState.Playing)
        {
            yield return m_CountdownWaitSec;
            m_CountDownTimerSec--;
            ui.UpdateCountDown(m_CountDownTimerSec.ToString());
            if(m_CountDownTimerSec<=0)
            {
                StopGame();
                ui.SetGameComplete(m_PlayerScore, m_TotalKilledZombie);
            }
        }
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
                        if(zombie.TakeDamage())
                        {
                            m_TotalKilledZombie++;
                            m_PlayerScore+= SCORE_PER_ZOMBIE;
                            ui.UpdatePlayerScore(m_PlayerScore.ToString());
                        }
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
            if(m_PlayerHealth<=0)
            {
                StopGame();
                ui.SetGameOver();
            }
        }
    }
    private void StopGame()
    {
        machineGun.gameObject.SetActive(false);
        m_ZombieSpawner.DeactivateAll();
        m_GameState=GameState.Stop;
    }
    public void Shoot()
    {
        isShoot = true;
    }
}
