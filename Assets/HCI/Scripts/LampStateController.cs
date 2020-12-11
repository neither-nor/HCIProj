using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LampStateController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lightItem;
    public GameObject textObj = null;
    public string label = "";
    void Start()
    {
        if (textObj != null)
        {
            textObj.GetComponent<TextMesh>().text = label;
        }
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
