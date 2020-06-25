using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    [SerializeField]
    private Button m_BackBtn;
    void Start()
    {
        m_BackBtn.onClick.AddListener(ClickBackButton);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void ClickBackButton()
    {
        Console.WriteLine("kepencet anjing");
        // reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // load the previous scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
