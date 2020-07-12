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

    public SpriteRenderer background;

    public Color previousBackgroundColor = Color.black;
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
        SceneManager.sceneLoaded += OnSceneLoaded;
        playerRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        originalColor = playerRenderer.color;
        //background = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        health = StartingHealth;
        score = 0;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        ResetGame();
    }

    public void Damage(int amount)
    {
        health -= amount;
        OnHealthChanged(health);
        StopCoroutine(HitPlayerRoutine());
        playerRenderer.color = originalColor;
        StartCoroutine(HitPlayerRoutine());
    }

    public static void SetBackgroundColor(Color color)
    {
        instance.StartCoroutine(instance.LerpToColor(color));
    }

    private IEnumerator LerpToColor(Color color)
    {
        var startColor = background.color;
        float duration = 1.5f;
        float curDuration = 0f;
        while (curDuration < duration)
        {
            var curColor = Color.Lerp(startColor, color, curDuration / duration);
            background.color = curColor;
            curDuration += Time.deltaTime;
            yield return null;
        }

        previousBackgroundColor = startColor;
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
        yield return new WaitForSecondsRealtime(2f);
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
        instance.health = instance.StartingHealth;
        instance.score = 0;
        Time.timeScale = 1f;
    }
}
