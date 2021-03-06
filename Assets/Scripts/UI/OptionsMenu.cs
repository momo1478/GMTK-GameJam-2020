﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OptionsMenu : MonoBehaviour
{
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider sfxVolume;
    public UnityEvent onOpen;
    public UnityEvent onClose;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject headerText;

    void Start() {
        SetSliders();
    }

    void SetSliders()
    {
        masterVolume.value = MixLevels.current.GetMasterSlider();
        musicVolume.value = MixLevels.current.GetMusicSlider();
        sfxVolume.value = MixLevels.current.GetSfxSlider();
    }

    public void SetMasterVolume(float volume)
    {        
        MixLevels.current.SetMasterVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        MixLevels.current.SetMusicVolume(volume);
    }

    public void SetSfxVolume(float volume)
    {
        MixLevels.current.SetSfxVolume(volume);
    }

    void SetActive(bool value)
    {
        SetSliders();
        gameObject.SetActive(value);
    }

    public void Close()
    {
        mainMenu.SetActive(true);
        headerText.SetActive(true);
        SetActive(false);
        onClose.Invoke();
    }

    public void Open()
    {
        mainMenu.SetActive(false);
        headerText.SetActive(false);
        SetActive(true);
        onOpen.Invoke();
    }
}
