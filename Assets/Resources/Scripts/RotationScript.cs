using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{

    Touch touch;
    public GameObject Gimmick;
    float gimmickRot;
    Vector3 dir;
    float angle;
    float lastAngle;
    void Awake()
    {
        Gimmick = transform.parent.gameObject;
        gameObject.SetActive(false);
    }
    void Start()
    {
        
    }
    void Update()
    {

        //print(Input.mousePosition);
    }
    void OnMouseDown()
    {
        //print("Rotation Down");
        TouchMng.instance.SetRotation(true);
        gimmickRot = Gimmick.transform.eulerAngles.z;
        print("Rotation Down");
        //print("gimmickrot: " + gimmickRot);

        dir = Input.mousePosition - Camera.main.WorldToScreenPoint(Gimmick.transform.position);

        lastAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
    void OnMouseDrag()
    {
        //print("Rotation Drag");

        TouchMng.instance.SetRotation(true);
        if (SystemInfo.deviceType == DeviceType.Desktop)
            dir = Input.mousePosition - Camera.main.WorldToScreenPoint(Gimmick.transform.position);
        else
            dir=new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y,0)- Camera.main.WorldToScreenPoint(Gimmick.transform.position);
        


        float newAngle =   Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float deltaAngle = lastAngle - newAngle;
        gimmickRot = gimmickRot - deltaAngle;
        //print("angle: " + gimmickRot);
        Gimmick.transform.rotation =  Quaternion.AngleAxis(gimmickRot, Vector3.forward);
        lastAngle = newAngle;

    }
    void OnMouseUp()
    {
        //print("Rotation Up");

        TouchMng.instance.SetRotation(false);
        lastAngle = 0;
    }
}