using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveLampStateController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lightItem;
    public GameObject textObj = null;
    public string label = "";

    public GameObject waveTable;
    public int xL, xR, zL, zR;
    
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
        Debug.Log("click " + xL + " " + xR + " " + zL + " " + zR);
        //lightItem.GetComponent<Light>().enabled = !lightItem.GetComponent<Light>().enabled;
        waveTable.GetComponent<WaveTable>().UpdateLight(xL, xR, zL, zR, lightItem, label);
    }
}
