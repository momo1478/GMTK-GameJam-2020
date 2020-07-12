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

    private Coroutine gameOver;


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
    }

    public void Damage(int amount)
    {
        health -= amount;
        OnHealthChanged(health);
        print($"(-) Health : {health}");
    }

    public void Heal(int amount)
    {
        health += amount;
        OnHealthChanged(health);
        print($"(+) Health : {health}");
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
        print("hAhA u LoSe!1!");
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
