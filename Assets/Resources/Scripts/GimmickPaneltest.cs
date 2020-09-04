using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GimmickPaneltest : MonoBehaviour
{
    public GameObject Env;
    GameObject newgimmick;
    Text RemainPanelText;
    Button button;
    /// <summary>
    /// 사용할 수 있는 남은 횟수
    /// </summary>
    [SerializeField]
    private int remaining;
    public GameObject prefab;

    public Text remainingText;

    void Awake()
    {
        //ex)ShowcaseTree ->Tree
        string trim = transform.name.Remove(0, 8);
        //print(trim);
        prefab = Resources.Load<GameObject>("Prefabs/Gimmicks/" + trim);
        
        RemainPanelText = transform.Find("RemainPanel").Find("Text").GetComponent<Text>();
        RemainPanelText.text = remaining.ToString();
        button = GetComponent<Button>();
        remainingText = transform.Find("RemainPanel").Find("Text").GetComponent<Text>();
    }
    void Start()
    {
        Env = GameObject.Find("Environment");

    }

    /// <summary>
    /// 아래 오브젝트 목록중 기믹을 클릭하여 생성하는 함수
    /// </summary>
    public void Click()
    {
        print("Showcase Click");

        if (GetRemaining() == 0)
            return;

        GameObject newobject = Instantiate(prefab, Env.transform);

        //ex)Tree (Clone) -> Tree
        newobject.name = prefab.name.Split(' ')[0].ToString();

        //오브젝트 위치 설정(카메라 한가운데)
        LocateNewobject(newobject);

        //오브젝트 선택해서 원(회전) 활성화
        newobject.GetComponent<MoveScript>().SelectObject(newobject, true);

        SetRemaining(-1);
        isEmpty();
    }

    public void LocateNewobject(GameObject ob)
    {
        Vector2 center = Camera.main.transform.position;
        print("카메라 Center: "+center);
        ob.transform.position = center;
    }

    /// <summary>
    /// 기믹이 삭제됨, 횟수 증가시킴
    /// </summary>
    public void Remove()
    {
        //횟수1증가
        SetRemaining(1);
        //버튼 활성화
        isEmpty();
    }
    public int GetRemaining()
    {
        return remaining;
    }
    /// <summary>
    /// 남은 횟수 갱신후 텍스트도 갱신
    /// </summary>
    /// <param name="i"></param>
    public void SetRemaining(int i)
    {
        remaining += i;
        remainingText.text = GetRemaining().ToString();

    }
    /// <summary>
    /// 비어있으면 버튼 비활성화, 남아있으면 버튼 활성화
    /// </summary>
    /// <returns></returns>
    public bool isEmpty()
    {
        if (remaining == 0)
        {
            button.interactable = false;
            return true;
        }
        else
        {
            button.interactable = true;
            return false;
        }
    }
}
