using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button m_ShootTapBtn;
    [SerializeField]
    private TMPro.TextMeshProUGUI m_bulletLabel;
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
}
