using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Remove : MonoBehaviour,IPointerUpHandler
{
    Transform tr;
    float resizedValue;
    
    void OnEnable()
    {
        ResizeBasedonFOV();
    }
    void Awake()
    {
        tr = transform;
    }
    void Start()
    {
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //print("Remove Btn Click");
    }

    /// <summary>
    /// 카메라의 FOV가 바뀜에 따라 삭제버튼의 크기를 조절한다.
    /// </summary>
    public void ResizeBasedonFOV()
    {
        float FOV = Camera.main.fieldOfView;
        resizedValue = (0.01f * FOV)+0.2f;
        Vector3 vec = new Vector3(resizedValue, resizedValue, resizedValue);
        tr.localScale = vec;
    }
}
