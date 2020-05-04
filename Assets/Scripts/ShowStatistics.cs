using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStatistics : MonoBehaviour
{
    public DuckBehaviour duckBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        duckBehaviour = FindObjectOfType<DuckBehaviour>();
    }
    public Slider nourishmentSlider;
    public Slider satiationSlider;
    public Slider healthSlider;
    // Update is called once per frame
    void Update()
    {
        nourishmentSlider.value = duckBehaviour.Nourishment;
        satiationSlider.value = duckBehaviour.Satiation;
        healthSlider.value = duckBehaviour.Health;
        if(duckBehaviour.Nourishment == 0)
        {
            
        }


    }
}
