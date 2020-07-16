using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    public static MixLevels current;
    public AudioMixer mixer;

    private void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(this);
        }
        else
        {
            current = this;
            DontDestroyOnLoad(this);
        }
    }

    float SliderToVolume(float value)
    {
        return Mathf.Log10(value) * 20;
    }

    float VolumeToSlider(float value)
    {
        return Mathf.Pow(10, value / 20);
    }

    public float GetMasterSlider()
    {
        float volume;
        mixer.GetFloat("MasterVolume", out volume);
        return VolumeToSlider(volume);
    }

    public float GetMusicSlider()
    {
        float volume;
        mixer.GetFloat("MusicVolume", out volume);
        return VolumeToSlider(volume);
    }

    public float GetSfxSlider()
    {
        float volume;
        mixer.GetFloat("SfxVolume", out volume);
        return VolumeToSlider(volume);
    }
    
    public void SetMasterVolume(float value)
    {
        mixer.SetFloat("MasterVolume", SliderToVolume(value));
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", SliderToVolume(value));
    }

        public void SetSfxVolume(float value)
    {
        mixer.SetFloat("SfxVolume", SliderToVolume(value));
    }
}
