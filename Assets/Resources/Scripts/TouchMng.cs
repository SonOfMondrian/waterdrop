using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

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
    public GameObject RemoveBtn;

    /// <summary>
    /// 현재 선택한 오브젝트
    /// </summary>
    [SerializeField]
    public GameObject SelectedObject;
    [SerializeField]
    private bool b_isSelect;
    [SerializeField]
    private bool b_isObDrag;
    [SerializeField]
    private bool b_isRotation;
    [SerializeField]
    private bool b_isCameraMoving;

    public bool b_isShowcasePress;

    [SerializeField]
    private bool b_isRemovePress;
    void Awake()
    {
        if (instance == null)
            instance = this;

        RemoveBtn = GameObject.Find("RemoveBtn");
        RemoveBtn.SetActive(false);
    }
    void Start()
    {
        cam = Camera.main;
    }

    public Vector2 GettouchPos()
    {
        int touchcnt = Input.touchCount;

        if (touchcnt == 1)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            int layerMask = 1 << LayerMask.NameToLayer("Panel");  // Panel 레이어만 충돌 체크함
            //print("layermask:" + layerMask);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 0.01f);
            if (Physics.Raycast(ray, out hit,layerMask))
            {
                print("GettouchPos(): "+hit.collider.name);
                return hit.point;
            }
            return Vector2.zero;
        }
        else
            return Vector2.zero;
    }

    void Update()
    {
        nTouch = Input.touchCount;

        //GimmickPanel에 있는 오브젝트 누르는 중이면 드래그 안되기하기
        //if (b_isShowcasePress)
        //    return;

        //일시정지일땐 터치 안먹게
        if (GameMng.instance.GetisPause() == true)
            return;

        if (Input.GetMouseButtonDown(0) && SystemInfo.deviceType == DeviceType.Desktop)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow, 0.01f);
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


            //Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow, 0.01f);
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
                        SetisRemovePress(false);
                    }

                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved && SelectedObject == null && b_isCameraMoving)
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
                    SetisRemovePress(false);

                }
            }
        }
        //2개 동시 터치일때
        else if (nTouch == 2)
        {
            b_isCameraMoving = false;
            DisableObject();

            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

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
            else if ((Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Stationary)||
                (Input.GetTouch(0).phase == TouchPhase.Stationary && Input.GetTouch(1).phase == TouchPhase.Moved)||
                (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved))
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

                if (FOV >= 80)
                    FOV = 80;
                else if (FOV <= 15)
                    FOV = 15;
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

    public void SetRemove(bool b)
    {
        RemoveBtn.SetActive(b);
        //RemoveBtn.transform.Find("Sprite").GetComponent<Remove>().ResizeBasedonFOV();
    }
    public void SetRemovePosition(Vector3 pos)
    {
        RemoveBtn.transform.position = pos;
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
            SetRemove(false);
        }
        
    }

    public void SelectObject(GameObject ob)
    {
        print("TouchMng SelectObject Method");
        SelectedObject = ob;
        b_isSelect = true;
        //선택될때 마다 FOV에 따라 삭제버튼을 리사이징한다.
        
    }
    public GameObject GetSelectedObject()
    {
        return SelectedObject;
    }
    public void SetDrag(bool b)
    {
        b_isObDrag = b;
    }
    public bool GetisDrag()
    {
        return b_isObDrag;
    }
    public void SetRotation(bool b)
    {
        b_isRotation = b;
    }
    public bool GetisRotation()
    {
        return b_isRotation;
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

    /// <summary>
    /// 삭제버튼을 눌러 오브젝트 삭제와 삭제버튼 비활성화
    /// </summary>
    public void RemoveObject()
    {
        print("Destroy");
        GetSelectedObject().GetComponent<MoveScript>().Showcase.GetComponent<GimmickPaneltest>().Remove();
        Destroy(SelectedObject);
        SetRemove(false);
    }

    public void SetisRemovePress(bool b)
    {
        b_isRemovePress = b;
    }
    public bool GetisRemovePress()
    {
        return b_isRemovePress;
    }
}
