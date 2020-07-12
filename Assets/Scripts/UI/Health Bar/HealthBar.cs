using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
    }

    void Start()
    {
        GameManager.OnHealthChanged += UpdateHealthBarUI;
        slider = GetComponent<Slider>();    
    }

    private void UpdateHealthBarUI(int obj)
    {
        float percentage = (float)obj / GameManager.instance.StartingHealth;
        DOTween.To(() => slider.value, (x) => slider.value = x, percentage, 0.2f);
    }
}
