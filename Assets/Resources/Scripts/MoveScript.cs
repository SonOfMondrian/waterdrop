using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


/// <summary>
/// 해당 스크립트는 기믹을 이동, 회전만을 위한 스크립트입니다. 그 외에는 사용되지 않습니다.
/// </summary>
public class MoveScript : MonoBehaviour
{
    public GameObject debug;
    public bool isDrag;
    public bool isSelect;
    public bool isPressed;
    GameObject SelectedImage;
    //GameObject RemoveBtn;
    public GameObject selectedobject;
    RaycastHit hit;
    Touch touch;

    Vector3 Downpos;
    float deltaX, deltaY;

    public Renderer ren;
    public int sort;
    void Awake()
    {
        SelectedImage = transform.Find("Rotation").gameObject;
        //RemoveBtn = transform.Find("Remove").gameObject;
        //RemoveBtn.SetActive(false);
    }
    void Start()
    {
        //ren = transform.Find("Model").GetComponent<MeshRenderer>();
        
    }

    void Update()
    {
        //ren.sortingOrder = sort;
        //debug.GetComponent<pos>().Print("Raycase Hit!");
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            
            Ray ray;

            if (SystemInfo.deviceType == DeviceType.Desktop)
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            else
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            //Offset이동을 위한 코드
            Downpos = Camera.main.ScreenToWorldPoint(touch.position);


            if (Physics.Raycast(ray,out hit,Mathf.Infinity))
            {
                //print(hit.point);
                if (hit.collider.tag == "Gimmick")
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        DeselectObject();
                        
                        TouchMng.instance.SelectObject(hit.collider.gameObject);
                        SelectObject(hit.collider.gameObject,false);
                        TouchMng.instance.SetDrag(true);



                        TouchMng.instance.SetRemovePosition(new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, transform.position.z));

                        deltaX = hit.point.x - TouchMng.instance.GetSelectedObject().transform.position.x;
                        deltaY = hit.point.y - TouchMng.instance.GetSelectedObject().transform.position.y;
                    }
                }
                else if(hit.collider.tag =="Remove")
                {
                    if(touch.phase==TouchPhase.Ended)
                    {
                        print("Remove");
                        TouchMng.instance.RemoveObject();
                    }
                }
            }
            if (touch.phase == TouchPhase.Moved && isPressed &&isSelect)
            {
                //print("gimmick Moved");
                TouchMng.instance.GetSelectedObject().transform.position= new Vector3(hit.point.x - deltaX, hit.point.y - deltaY, transform.position.z);

                TouchMng.instance.RemoveBtn.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                isPressed = false;
                TouchMng.instance.SetDrag(false);
            }
        }
    }
    /// <summary>
    /// 선택되면 선택 활성화 원(원을 드래그하여 회전)이 생긴다.
    /// </summary>
    public void SelectObject(GameObject select,bool iscreated)
    {
        //print(select.name);
        MoveScript Script = select.GetComponent<MoveScript>();

        Script.SelectedImage.SetActive(true);
        Script.isSelect = true;

        if(!iscreated)
            Script.isPressed = true;

        Script.selectedobject = select;
        TouchMng.instance.SetRemove(true);
        TouchMng.instance.SetRemovePosition(transform.position);


        TouchMng.instance.SelectObject(select);
    }

    public void DeselectObject()
    {
        SelectedImage.SetActive(false);
        isSelect = false;
        isPressed = false;
        selectedobject = null;

    }

}
