using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public float maximum;
    public float current; 

    public Image mask;
    
    public void SetCurrent(float value) {        
        current = Mathf.Clamp(value, 0, maximum);
    }

    void Update()
    {      
        mask.fillAmount = current / maximum;
    }


}
