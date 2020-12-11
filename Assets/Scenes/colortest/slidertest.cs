using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
public class slidertest : MonoBehaviour
{
    public Slider hueSilder;

    double value;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnSliderUpdated(SliderEventData eventData)
    {
        value = eventData.NewValue;
        hueSilder.value = (float)value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
