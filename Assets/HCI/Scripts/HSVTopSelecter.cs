using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSVTopSelecter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject waveTable;
    void Start()
    {
        
    }

    public void OnSelect()
    {
        Debug.Log("select top");
        waveTable.GetComponent<WaveTable>().SelectHSVTop();  
    }

    public void OnSideSelect()
    {
        Debug.Log("select side");
        waveTable.GetComponent<WaveTable>().SelectHSVSide();  
    }
}
