using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static event Action<int> OnHealthChanged = delegate { };
    public Coroutine gameOver = null;

    [Header("Starting Stats")]
    public int StartingHealth = 25;

    [Header("Miscellaneous")]
    public Color hitColor;

    private int health;
    private float score;
    private SpriteRenderer playerRenderer;
    private Color originalColor;

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
        playerRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        originalColor = playerRenderer.color;
        health = StartingHealth;
        score = 0;
    }

    public void Damage(int amount)
    {
        health -= amount;
        OnHealthChanged(health);
        StopCoroutine(HitPlayerRoutine());
        playerRenderer.color = originalColor;
        StartCoroutine(HitPlayerRoutine());
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

    IEnumerator HitPlayerRoutine()
    {
        Color originalColor = playerRenderer.color;

        for (int i = 0; i < 3; i++)
        {
            playerRenderer.color = hitColor;
            yield return new WaitForSeconds(0.2f);
            playerRenderer.color = originalColor;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public static int GetScore()
    {
        return (int) instance.score;
    }

    public static void AddScore(float rate)
    {
        if(instance.gameOver == null)
            instance.score += rate;
    }

    public static void ResetGame()
    {
        instance.gameOver = null;
        instance.health = 0;
        instance.score = 0;
    }
}
