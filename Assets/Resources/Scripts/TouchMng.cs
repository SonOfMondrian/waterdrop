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

    Vector3 lastpos;
    Vector3 newpos;
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
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);


            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow, 0.01f);
            //뭔가 맞으면
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                    raytext.GetComponent<pos>().Print(hit.collider.name + " / Phone 충돌");

                if (hit.collider.tag == "Panel")
                {

                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        print("Bagan");

                        //선택된 오브젝트를 해제
                        b_isSelect = false;
                        SelectedObject.GetComponent<MoveScript>().isSelect = false;
                        SelectedObject.GetComponent<MoveScript>().isPressed = false;
                        SelectedObject = null;
                        


                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Moved && SelectedObject==null)
                    {
                        raytext.GetComponent<pos>().Print("드래그");
                        print("Moved");

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
                        prevPos = Vector2.zero;
                        prevDistance = 0;
                    }

                }
                //else if (hit.collider.tag == "Gimmick")
                //{
                //    if (Input.GetTouch(0).phase == TouchPhase.Began)
                //    {
                //        print("기믹 Bagan");
                //        b_isSelect = true;
                //        SelectedObject = hit.collider.gameObject;
                //    }
                //    else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                //    {
                //        print("기믹 Moved");

                //        //hit.collider.transform = 
                        

                //    }
                //    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                //    {
                //        print("기믹 Ended");

                //    }
                //}
            }
        }
        //2개 동시 터치일때
        else if (nTouch == 2)
        {

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
                print("Zoom Began");
                //ZoomLastdis = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                raytext.GetComponent<pos>().Print("줌아웃");
                print("Zoom Moved");


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
                print("Zoom Ended");
                prevPos = Vector2.zero;
                ZoomLastdis = 0;
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
