using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.MixedReality.Toolkit.UI;
public class CurtainController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject curtainL, curtainR;
    public Vector3 startL, endL, startR, endR;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSliderUpdated(SliderEventData eventData)
    {
        curtainL.transform.position = startL * eventData.NewValue + endL * (1f - eventData.NewValue);
        curtainR.transform.position = startR * eventData.NewValue + endR * (1f - eventData.NewValue);
    }
}
