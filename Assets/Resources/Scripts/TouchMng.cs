using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMng : MonoBehaviour
{
    public static TouchMng instance;

    /// <summary>
    /// 줌 이전 프레임 두 점 사이의 거리
    /// </summary>
    float ZoomLastdis = 0;
    //줌 현재 프레임 두 점 사이의 거리
    float ZoomNewposdis = 0;
    public float MovePower;
    public float ZoomPower;
    Camera cam;
    Vector2 prevPos;
    public int nTouch;
    float prevDistance = 0;


    public GameObject raytext;

    /// <summary>
    /// 현재 선택한 오브젝트
    /// </summary>
    [SerializeField]
    public GameObject SelectedObject;
    [SerializeField]
    private bool b_isSelect;
    private bool b_isObDrag;
    [SerializeField]
    private bool b_isRotation;
    [SerializeField]
    private bool b_isCameraMoving;



    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        nTouch = Input.touchCount;

        if (Input.GetMouseButtonDown(0) && SystemInfo.deviceType == DeviceType.Desktop)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    raytext.GetComponent<pos>().Print(hit.collider.name + "/ Desktop 충돌");

                }
                if (hit.collider.tag == "Panel")
                {
                    raytext.GetComponent<pos>().Print("드래그");
                }
            }
        }

        if (nTouch == 1)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);


            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow, 0.01f);
            //뭔가 맞으면
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                    raytext.GetComponent<pos>().Print(hit.collider.name + " / Phone 충돌");

                if (b_isRotation)
                    return;

                if (hit.collider.tag == "Panel")
                {

                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        //print("Bagan");
                        b_isCameraMoving = true;


                        //선택된 오브젝트를 해제
                        DisableObject();
                    }

                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved && SelectedObject == null &&b_isCameraMoving)
                {
                    raytext.GetComponent<pos>().Print("드래그");
                    print("드래깅");
                    if (prevPos == Vector2.zero)
                    {
                        prevPos = Input.GetTouch(0).position;
                        return;
                    }
                    Vector2 dir = (Input.GetTouch(0).position - prevPos);
                    Vector3 vec = new Vector3(dir.x, dir.y, 0);
                    cam.transform.position -= cam.fieldOfView * 0.01f * vec * MovePower * Time.deltaTime;
                    prevPos = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    //print("Ended");
                    prevPos = Vector2.zero;
                    prevDistance = 0;
                    b_isCameraMoving = false;
                }
            }
        }
        //2개 동시 터치일때
        else if (nTouch == 2)
        {
            b_isCameraMoving = false;
            DisableObject();

            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            RaycastHit hit1;
            RaycastHit hit2;
            Ray ray1 = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Ray ray2 = Camera.main.ScreenPointToRay(Input.GetTouch(1).position);

            Debug.DrawRay(ray1.origin, ray1.direction * 100f, Color.yellow, 0.01f);
            Debug.DrawRay(ray2.origin, ray2.direction * 100f, Color.yellow, 0.01f);


            if ((Input.GetTouch(0).phase == TouchPhase.Stationary && Input.GetTouch(1).phase == TouchPhase.Began) ||
                (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Stationary) ||
                (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Began))
            {
                //print("Zoom Began");
                //ZoomLastdis = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                raytext.GetComponent<pos>().Print("줌아웃");
                //print("Zoom Moved");


                if(ZoomLastdis==0)
                {
                    ZoomLastdis = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    return;
                }
                ZoomNewposdis = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

                float delta = ZoomLastdis - ZoomNewposdis;
                float FOV = cam.fieldOfView;
                FOV += delta * ZoomPower;

                if (FOV >= 100)
                    FOV = 100;
                else if (FOV <= 16)
                    FOV = 16;
                cam.fieldOfView = FOV;

                ZoomLastdis = ZoomNewposdis;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                //print("Zoom Ended");
                prevPos = Vector2.zero;
                ZoomLastdis = 0;
            }
        }
    }

    /// <summary>
    /// 현재 선택된 기믹 오브젝트를 해제함
    /// </summary>
    public void DisableObject()
    {
        if (SelectedObject != null)
        {
            b_isSelect = false;
            b_isRotation = false;
            SelectedObject.GetComponent<MoveScript>().DeselectObject();
            SelectedObject = null;
        }
        
    }

    public void SelectObject(GameObject ob)
    {
        SelectedObject = ob;
        b_isSelect = true;
    }
    public GameObject GetSelectedObject()
    {
        return SelectedObject;
    }
    public void SetDrag(bool b)
    {
        b_isObDrag = b;
    }
    public void SetRotation(bool b)
    {
        b_isRotation = b;
    }
    private void DeselectOB()
    {
        b_isSelect = false;
        //SelectedObject.GetComponent<MoveScript>().DeselectObject();
    }

    public void ExitDrag()
    {
        prevPos = Vector2.zero;
        prevDistance = 0;
    }
}
