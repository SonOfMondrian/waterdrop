using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class pos : MonoBehaviour
{
    public GameObject a;
    public bool istouch;
    public bool nothing;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nothing)
            return; 

        
        if (SystemInfo.deviceType==DeviceType.Handheld&& istouch)
            GetComponent<Text>().text = Input.GetTouch(0).position.ToString() + ", " + Input.touchCount.ToString();
        else if(a!=null)
            GetComponent<Text>().text = a.transform.position.ToString();
    }
    public void Print(object message)
    {
        GetComponent<Text>().text = message.ToString();
    }
}
