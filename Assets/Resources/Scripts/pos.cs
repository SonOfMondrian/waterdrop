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

        
        //if (istouch)
        //{
        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    Physics.Raycast(ray, out hit);
        //    GetComponent<Text>().text = hit.point.ToString();
        //    //if (SystemInfo.deviceType == DeviceType.Handheld)
        //    //{
                
        //    //}
        //    //else
        //    //    GetComponent<Text>().text = "Mouse: " + Input.mousePosition.ToString();
        //}
            
        else if(a!=null)
            GetComponent<Text>().text = a.transform.position.ToString();
    }
    public void Print(object message)
    {
        GetComponent<Text>().text = message.ToString();
    }
}
