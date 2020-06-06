using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button m_ShootTapBtn;
    [SerializeField]
    private TextMeshProUGUI m_bulletLabel;
    [SerializeField]
    private TextMeshProUGUI m_playerHealthLabel;
    [SerializeField]
    private GameObject[] icons;
    private GameManager game;
    // Start is called before the first frame update
    void Start()
    {
        m_ShootTapBtn.onClick.AddListener(ClickShootTapBtn);
    }

    private void ClickShootTapBtn()
    {
        game.Shoot();
    }

    public void SetGame(GameManager game)
    {
        this.game = game;
    }
    public void UpdateBulletLabel(string label)
    {
        m_bulletLabel.text = label;
    }
    public void UpdatePlayerHealthLabel(string label)
    {
        m_playerHealthLabel.text = label;
    }
    public void HideAll()
    {
        foreach (var item in icons)
        {
            item.SetActive(false);
        }
        m_ShootTapBtn.gameObject.SetActive(false);
        m_bulletLabel.gameObject.SetActive(false);
        m_playerHealthLabel.gameObject.SetActive(false);
    }
}
