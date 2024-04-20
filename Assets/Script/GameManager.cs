using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playGame;


    [SerializeField] private GameObject gamePanel;

    [SerializeField] private GameObject retryText;
    [SerializeField] private GameObject retryButton;

    [SerializeField] private GameObject gameOverButton;
    [SerializeField] private GameObject gameOverText;


    public void GameOver()
    {
        gamePanel.SetActive(true);
        gameOverButton.SetActive(true);
        gameOverText.SetActive(true);


    }

    public void StartScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);

    }

    public void GameWin()
    {
        gamePanel.SetActive(true);

        retryButton.SetActive(true);
        retryText.SetActive(true);
    }

}
