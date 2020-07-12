using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUI : MonoBehaviour
{

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;

    public static string[] GameOverText = new string[] {
        "Game over friend",
        "Better luck next time"
    };

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = GameManager.GetScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMainMenuClick()
    {
        GameManager.ResetGame();
        SceneManager.LoadScene("TitleScreen");
        Time.timeScale = 1f;
    }

    public void OnRetryClick()
    {
        GameManager.ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
