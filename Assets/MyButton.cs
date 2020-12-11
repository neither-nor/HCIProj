using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton : MonoBehaviour
{
    // Start is called before the first frame update
    public bool state = false;
    public GameObject state_on;
    public GameObject state_off;
    void Start()
    {
        ud();
    }
    void ud()
    {
        if (state)
        {
            state_on.SetActive(true);
            state_off.SetActive(false);
        }
        else
        {
            state_on.SetActive(false);
            state_off.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("Click");
        state = !state;
        ud();   
    }
}
