using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public bool loadOnAwake = false;
    public static LevelManager instance;
    public GameObject loadingScreen;
    public ProgressBar progressBar;
    public float minimumLoadScreenTimeSeconds = 1;

    private bool hasPersistent = false;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        if(SceneManager.GetActiveScene().buildIndex == (int) SceneIndices.TITLE_SCREEN){
            AudioManager.Instance.PlayMenuMusic();
        } else if (SceneManager.GetActiveScene().buildIndex == (int) SceneIndices.GAME){
            AudioManager.Instance.PlayGameMusic();
        }
    }

    public void LoadGame() {
        var gameScene = (int) SceneIndices.GAME;
        SceneManager.LoadScene(gameScene);
        AudioManager.Instance.PlayGameMusic();
    }


    public void LoadMenu() {
        var titleScene = (int) SceneIndices.TITLE_SCREEN;
        SceneManager.LoadScene(titleScene);
        AudioManager.Instance.PlayMenuMusic();
    }
}