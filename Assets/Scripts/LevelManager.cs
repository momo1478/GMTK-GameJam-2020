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

            var activeScene = SceneManager.GetActiveScene();

            if (loadOnAwake) {
                StartCoroutine(LoadMainMenuScene());
            }
            else {
                if (activeScene.buildIndex == (int) SceneIndices.PERSISTENT) {
                    hasPersistent = true;
                }

                if (!hasPersistent) {
                    hasPersistent = true;
                    SceneManager.LoadSceneAsync((int) SceneIndices.PERSISTENT, LoadSceneMode.Additive);
                }
            }
        }
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame() {
        StartCoroutine(LoadGameScene());
    }
    
    private IEnumerator LoadGameScene() {
        loadingScreen.gameObject.SetActive(true);
        
        var gameScene = (int) SceneIndices.GAME;
        var titleScene = (int) SceneIndices.TITLE_SCREEN;
        
        if (SceneManager.GetSceneByBuildIndex(titleScene).isLoaded)
            scenesLoading.Add(SceneManager.UnloadSceneAsync(titleScene));
        
        scenesLoading.Add(SceneManager.LoadSceneAsync(gameScene, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
        
        while (scenesLoading.Count > 0) {
            yield return null;
        }
        
        AudioManager.Instance.PlayGameMusic();
    }

    public void LoadMainMenu() {
        StartCoroutine(LoadMainMenuScene());
    }

    public IEnumerator LoadMainMenuScene() {
        loadingScreen.gameObject.SetActive(true);
        var gameScene = (int) SceneIndices.GAME;
        var titleScene = (int) SceneIndices.TITLE_SCREEN;
        
        if (SceneManager.GetSceneByBuildIndex(gameScene).isLoaded)
            scenesLoading.Add(SceneManager.UnloadSceneAsync(gameScene));
        
        scenesLoading.Add(SceneManager.LoadSceneAsync(titleScene, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());

        while (scenesLoading.Count > 0) {
            yield return null;
        }
        
        AudioManager.Instance.PlayMenuMusic();
    }

    public IEnumerator GetSceneLoadProgress() {
        float totalLoadingProgress = 0;

        float startTime = Time.time;

        foreach (AsyncOperation operation in scenesLoading) {
            while (!operation.isDone) {
                totalLoadingProgress = 0;
                foreach (AsyncOperation operation1 in scenesLoading) {
                    totalLoadingProgress += operation1.progress / scenesLoading.Count;
                }

                progressBar.SetCurrent(totalLoadingProgress * progressBar.maximum);
                yield return null;
            }
        }

        progressBar.SetCurrent(progressBar.maximum);

        float loadTimeSeconds = Time.time - startTime;
        if (loadTimeSeconds < minimumLoadScreenTimeSeconds) {
            yield return new WaitForSecondsRealtime(minimumLoadScreenTimeSeconds - loadTimeSeconds);
        }

        loadingScreen.gameObject.SetActive(false);
        scenesLoading.Clear();
    }
}