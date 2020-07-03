using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


/// <summary>
/// 해당 스크립트는 기믹을 이동, 회전만을 위한 스크립트입니다. 그 외에는 사용되지 않습니다.
/// </summary>
public class MoveScript : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public GameObject debug;
    public bool isDrag;
    public bool isSelect;
    public bool isPressed;
    GameObject SlectedImage;

    Touch touch;
    public float speedModifier;

    void Awake()
    {
        SlectedImage = transform.Find("Selected").gameObject;

    }
    void Start()
    {
        speedModifier = 0.01f;
    }

    void Update()
    {
        //debug.GetComponent<pos>().Print("Raycase Hit!");
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);


            if(Physics.Raycast(ray,out hit,Mathf.Infinity))
            {
                //print(hit.point);
                if (hit.collider.tag == "Gimmick")
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        TouchMng.instance.SelectObject(this.gameObject);
                        isSelect = true;
                        isPressed = true;
                    }
                    if (touch.phase == TouchPhase.Moved)
                    {
                        print("기믹 이동중");
                        debug.GetComponent<pos>().Print("Gimmick Moving!");

                        //transform.position = new Vector3(hit.point.x, hit.point.y, transform.position.z);
                    }
                }
            }
            if (touch.phase == TouchPhase.Moved && isSelect)
            {
                transform.position = new Vector3(hit.point.x, hit.point.y, transform.position.z);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                isPressed = false;
            }



        }
    }
    public void select()
    {
        print("기믹 클릭");
    }
    //public void OnMouseDown()
    //{
    //    isSelect = true;
    //    TouchMng.instance.SelectObject(this.gameObject);
    //    Debug.Log("다운");
    //}
    public void DeselectObject()
    {
        isSelect = false;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("기믹 클릭");
    }

    public void OnPointerUp(PointerEventData eventData)
    {


    }
    //void OnMouseUp()
    //{
    //    isDrag = false;
    //    Debug.Log("업");
    //}
    //private void OnMouseDrag()
    //{
    //    isDrag = true;
    //    TouchMng.instance.SetDrag(true);

    //    Debug.Log("드래그");
    //    Vector3 mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,10);

    //    transform.position = Camera.main.ScreenToWorldPoint(mousepos);
    //}
}
