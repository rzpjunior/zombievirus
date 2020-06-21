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
    private GameObject m_GameplayUI;
    [SerializeField]
    private TextMeshProUGUI m_CountDownLabel;
    [SerializeField]
    private TextMeshProUGUI m_ScoreLabel;
    private GameManager game;
    [SerializeField]
    private GameCompleteUI m_gameCompleteUI;
    [SerializeField]
    private GameOverUI m_gameOverUI;
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
    public void UpdateCountDown(string label)
    {
        m_CountDownLabel.text = label;
    }
    public void UpdatePlayerScore(string label)
    {
        m_ScoreLabel.text = label;
    }
    public void UpdatePlayerHealthLabel(string label)
    {
        m_playerHealthLabel.text = label;
    }
    private void HideGameplayUI()
    {
        m_GameplayUI.SetActive(false);
    }
    public void SetGameComplete(int playerScore, int zombieKilled)
    {
        HideGameplayUI();
        m_gameCompleteUI.Show(playerScore, zombieKilled);
    }
    public void SetGameOver()
    {
        HideGameplayUI();
        m_gameOverUI.Show();
    }
}
