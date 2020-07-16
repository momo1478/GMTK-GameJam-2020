using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public OptionsMenu menu;

    float timescale = 1;

    public void Awake() {
        timescale = Time.timeScale;
    }


    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ui.SetActive(true);
            menu.Open();
        }
    }


    public void Pause() {
        timescale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void Unpause() {
        ui.SetActive(false);
        Time.timeScale = timescale;
    }

    public void MainMenu() 
    {
        LevelManager.instance.LoadMenu();
    }
}
