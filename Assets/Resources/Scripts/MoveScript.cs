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
    GameObject SlectedImage;
    float deltaX = 0, deltaY = 0;
    void Awake()
    {
        SlectedImage = transform.Find("Selected").gameObject;

    }
    void Start()
    {
        //thisgob = this.gameObject;
    }

    void Update()
    {
        ////debug.GetComponent<pos>().Print("Raycase Hit!");
        //if (Input.touchCount > 0)
        //{
        //    Vector3 touchPos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);

        //    Ray ray = Camera.main.ScreenPointToRay(touchPos);
        //    print(ray);

        //    RaycastHit hit;

        //    //레이 쏴서 먼가 맞으면
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //    {
        //        debug.GetComponent<pos>().Print(hit.collider.name + " Raycast Hit!");
        //        TouchMng.instance.SetDrag(true);

                
        //        if (Input.GetTouch(0).phase == TouchPhase.Began)
        //        {
        //            debug.GetComponent<pos>().Print(hit.collider.name + " Began!");

        //            isSelect = true;
        //            TouchMng.instance.SelectObject(this.gameObject);
        //            deltaX = touchPos.x - transform.position.x;
        //            deltaY = touchPos.y - transform.position.y;

        //        }
        //        else if (Input.GetTouch(0).phase == TouchPhase.Moved)    // 터치하고 움직이믄 발생한다.
        //        {
        //            debug.GetComponent<pos>().Print(hit.collider.name + " Moved!");

        //            Vector3 mousepos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

        //            transform.position = Camera.main.ScreenToWorldPoint(mousepos)-new Vector3(deltaX,deltaY,-10);

        //        }

        //        else if (Input.GetTouch(0).phase == TouchPhase.Ended)    // 터치 따악 떼면 발생한다.
        //        {
        //            debug.GetComponent<pos>().Print(hit.collider.name + " Ended!");

        //            TouchMng.instance.SetDrag(false);
        //        }
        //    }
        //    else
        //    {
        //        debug.GetComponent<pos>().Print("Nothing Hit!");
        //        DeselectObject();
        //    }
        //}

        //if (isSelect)
        //{
        //    SlectedImage.SetActive(true);
        //}
        //else
        //    SlectedImage.SetActive(false);
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
