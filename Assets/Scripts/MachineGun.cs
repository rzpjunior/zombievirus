using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    [SerializeField]
    private GameObject m_flash;
    [SerializeField]
    private Animator m_Animator;
    private const int MAX_BULLET = 20;
    private int m_BulletCounter = MAX_BULLET;
    private WaitForSeconds m_waitShowFlash = new WaitForSeconds(0.1f);
    private WaitForSeconds m_reloadAnimationSec = new WaitForSeconds(1.10f);
    private UIManager ui;
    public int ValidateShoot()
    {
        int currentBullet = m_BulletCounter;
        if(m_BulletCounter>0)
        {
            m_BulletCounter--;
            UpdateBullet();
            StartCoroutine(PlayFlash());
            m_Animator.Play("MachineGin_shoot");
        }
        else
        {
            StartCoroutine(Reloading());
        }
        return currentBullet;
        
    }

    IEnumerator PlayFlash()
    {
        m_flash.SetActive(true);
        yield return m_waitShowFlash;
        m_flash.SetActive(false);
    }

    IEnumerator Reloading()
    {
        m_Animator.Play("MachineGun_reload");
        yield return m_reloadAnimationSec;
        m_BulletCounter=MAX_BULLET;
        UpdateBullet();
    }

    public void SetUI(UIManager ui)
    {
        this.ui = ui;
    }
    public void UpdateBullet()
    {
        string label= m_BulletCounter+"/"+MAX_BULLET;
        ui.UpdateBulletLabel(label);
    }

}
