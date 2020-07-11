using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnPlayClicked() {
        FindObjectOfType<LevelManager>().LoadGame();
    }

    public void OnExitClicked() {
        Application.Quit();
    }
}
