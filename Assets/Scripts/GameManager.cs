using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static event Action<int> OnHealthChanged = delegate { };

    [Header("Starting Stats")]
    public int StartingHealth = 25; 

    private int health;
    public float score;

   

    public Coroutine gameOver = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = StartingHealth;
        score = 0;
    }

    public void Damage(int amount)
    {
        health -= amount;
        OnHealthChanged(health);
    }

    public void Heal(int amount)
    {
        health += amount;
        OnHealthChanged(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && gameOver == null)
        {
            gameOver = StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 0f;
        Instantiate(
            Resources.Load<LoseUI>("UI/LoseUI"),
            GameObject.Find("Canvas").GetComponent<Transform>()
        );
    }

    public static int GetScore()
    {
        return (int) instance.score;
    }

    public static void AddScore(float rate)
    {
        instance.score += rate;
    }

    public static void ResetGame()
    {
        instance.gameOver = null;
    }
}
