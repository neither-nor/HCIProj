using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
public class updateLightColor : MonoBehaviour
{
    public Light light;
    public ColorPicker hsvpicker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updateBrightness(SliderEventData eventData)
    {
        light.intensity = eventData.NewValue * 5;
    }

    public void updateLight(SliderEventData eventData)
    {
        light.color = hsvpicker.CurrentColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
