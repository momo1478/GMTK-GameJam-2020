using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[ExecuteInEditMode()]
public class Card : MonoBehaviour
{
    public Image border;
    public UnityEvent onClick;

    void Start()
    {        
        border.gameObject.SetActive(false);
    }

    public void OnClicked()
    {
        onClick.Invoke();
    }

    public void Select()
    {
        border.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        border.gameObject.SetActive(false);
    }
}
