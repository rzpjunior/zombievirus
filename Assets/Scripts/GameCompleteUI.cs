using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCompleteUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ScoreLabel;
    [SerializeField]
    private TextMeshProUGUI m_ZombieKilledLabel;
    [SerializeField]
    private Button m_SaveScoreBtn;
    private int m_PlayerScore;
    void Start()
    {
        m_SaveScoreBtn.onClick.AddListener(ClickSaveScoreBtn);
    }

    public void Show(int playerScore, int zombieKilled)
    {
        m_PlayerScore = playerScore;
        m_ScoreLabel.text = m_PlayerScore.ToString();
        m_ZombieKilledLabel.text = zombieKilled.ToString();
        gameObject.SetActive(true);
    }

    private void ClickSaveScoreBtn()
    {
        PlayerPrefs.SetInt("PLAYER_SCORE", m_PlayerScore);
    }
}
