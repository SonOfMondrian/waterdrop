using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMng : MonoBehaviour
{
    public static TouchMng instance;
    Vector3 mousestart;
    float m_fOldToucDis = 0f;       // 터치 이전 거리를 저장합니다.
    float m_fFieldOfView = 60f;     // 카메라의 FieldOfView의 기본값을 60으로 정합니다.
    public float MovePower;
    public float ZoomPower;
    Camera cam;
    Vector2 prevPos;
    public int nTouch;
    float prevDistance = 0;

    public Vector3 lastpos;
    public Vector3 newpos;
    TouchPhase touchphase = TouchPhase.Ended;

    public GameObject raytext;

    /// <summary>
    /// 현재 선택한 오브젝트
    /// </summary>
    [SerializeField]
    public GameObject SelectedObject;
    [SerializeField]
    private bool b_isSelect;
    private bool b_isObDrag;

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
            Ray ray = Camera.main.ScreenPointToRay(touchPos);

            //뭔가 맞으면
            if (Physics.Raycast(ray, out hit))
            {
                

                if (hit.collider != null)
                    raytext.GetComponent<pos>().Print(hit.collider.name + " / Phone 충돌");
                if (hit.collider.tag == "Panel")
                {
                    //raytext.GetComponent<pos>().Print("드래그");
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        print("Bagan");
                        //lastpos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, cam.transform.position.z);


                        //mousestart = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, cam.transform.position.z);
                        //mousestart = Camera.main.ScreenToWorldPoint(mousestart);
                        //mousestart.z = Camera.main.transform.position.z;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        print("Moved");
                        //newpos= new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, cam.transform.position.z);
                        //Vector3 delta = cam.ScreenToWorldPoint(lastpos - newpos);
                        //Vector3 move = new Vector3(delta.x * MovePower, delta.y * MovePower, 0);
                        ////cam.transform.Translate(move, Space.World);
                        //cam.transform.position = move;
                        //lastpos = newpos;


                        //var mousemove = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, cam.transform.position.z);
                        //mousemove = Camera.main.ScreenToWorldPoint(mousemove);
                        //mousemove.z = Camera.main.transform.position.z;

                        //cam.transform.position = cam.transform.position + (mousemove - mousestart);
                        //===============================================================

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
                        print("Ended");
                        //lastpos = Vector3.zero;
                       // newpos = Vector3.zero;

                        prevPos = Vector2.zero;
                        prevDistance = 0;
                    }

                }
                else if (hit.collider.tag == "Gimmick")
                {
                    // if()
                }
            }
        }
        //2개 동시 터치일때
        else if(nTouch==2)
        {
            Vector3 touchPos1 = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector3 touchPos2 = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
            RaycastHit hit1;
            RaycastHit hit2;
            Ray ray1 = Camera.main.ScreenPointToRay(touchPos1);
            Ray ray2 = Camera.main.ScreenPointToRay(touchPos2);

            if(Physics.Raycast(ray1, out hit1)&& Physics.Raycast(ray2, out hit2))
            {
                if(hit1.collider.tag=="Panel"&& hit2.collider.tag == "Panel")
                {
                    

                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        raytext.GetComponent<pos>().Print("줌아웃");
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {

                    }
                }
            }


        }
    }

    public void SelectObject(GameObject ob)
    {
        SelectedObject = ob;
        b_isSelect = true;
    }
    public void SetDrag(bool b)
    {
        b_isObDrag = b;
    }
    private void DeselectOB()
    {
        b_isSelect = false;
        //SelectedObject.GetComponent<MoveScript>().DeselectObject();
    }

    //public void Drag()
    //{


    //    //터치가 하나이고, 움직이는 중이라면 드래그한 방향으로 카메라를 이동시킨다.
    //    Debug.Log("카메라 이동");
    //    if (nTouch == 1 && Input.GetTouch(0).phase==TouchPhase.Ended)
    //    {
    //        Vector3 touchPos= Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    //        //Vector2 touchPos2D = new Vector2(touchPos.x, touchPos.y);
    //        RaycastHit hit;
    //        Ray ray = Camera.main.ScreenPointToRay(touchPos);

    //        //뭔가 맞으면
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if (hit.collider != null)
    //                raytext.GetComponent<pos>().Print(hit.collider.name + "충돌");
    //        }

    //        //if (prevPos == Vector2.zero)
    //        //{
    //        //    prevPos = Input.GetTouch(0).position;
    //        //    return;
    //        //}

    //        //Vector2 dir = (Input.GetTouch(0).position - prevPos);
    //        //Vector3 vec = new Vector3(dir.x, dir.y, 0);

    //        //cam.transform.position -= cam.fieldOfView*0.01f* vec * MovePower * Time.deltaTime;


    //        //prevPos = Input.GetTouch(0).position;
    //    }
    //    else if(nTouch==2)
    //    {
    //        //선택된 오브젝트 해제
    //        DeselectOB();
    //        if (prevDistance==0)
    //        {
    //            prevDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
    //            return;
    //        }
    //        float curDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
    //        float move = prevDistance - curDistance;

    //        float FOV = cam.fieldOfView;
    //        FOV += move * ZoomPower * Time.deltaTime;


    //        if (FOV >= 100)
    //            FOV = 100;
    //        else if (FOV <= 20)
    //            FOV = 20;
    //        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, FOV, 1.0f);


    //        prevDistance = curDistance;
    //    }

    //}
    public void ExitDrag()
    {
        prevPos = Vector2.zero;
        prevDistance = 0;
    }
}
