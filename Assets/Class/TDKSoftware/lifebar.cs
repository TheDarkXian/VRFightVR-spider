using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifebar : MonoBehaviour {
    [SerializeField]
    UniversalCharcter charcter;
    [SerializeField]
    Slider slider;
    // Use this for initialization
    private void Start()
    {
        charcter.health.OnValueChanged.AddListener(UpdateLifeBar);
    }
    public void UpdateLifeBar(float value ) {
                                             
        slider.value = charcter.health.Process();

    }
    void OnEnable()
    {
        slider.value = 1;    
    }
}
