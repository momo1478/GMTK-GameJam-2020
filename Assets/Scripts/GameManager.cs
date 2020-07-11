using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject loadingScreen;
    public ProgressBar progressBar;
    public float minimumLoadScreenTimeSeconds = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.LoadSceneAsync((int)SceneIndices.TITLE_SCREEN, LoadSceneMode.Additive);        
        } else {
            Destroy(gameObject);
        }
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void LoadGame()
    {
        loadingScreen.gameObject.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndices.TITLE_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndices.GAME, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
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
        if (loadTimeSeconds < minimumLoadScreenTimeSeconds)
        {
            yield return new WaitForSecondsRealtime(minimumLoadScreenTimeSeconds - loadTimeSeconds);
        }
        
        loadingScreen.gameObject.SetActive(false);
    }
}
