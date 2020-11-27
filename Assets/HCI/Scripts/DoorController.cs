using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    public bool isOpening = false;
    void Start()
    {
        isOpening = !isOpening;
        Click();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (isOpening)
        {
            door.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        else
        {
            door.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        }
        isOpening = !isOpening;
    }
}
