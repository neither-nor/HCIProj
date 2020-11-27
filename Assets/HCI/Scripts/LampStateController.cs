using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LampStateController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lightItem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonClick()
    {
        lightItem.GetComponent<Light>().enabled = !lightItem.GetComponent<Light>().enabled;
    }
}
