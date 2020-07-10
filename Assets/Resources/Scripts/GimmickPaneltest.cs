using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GimmickPaneltest : MonoBehaviour
{
    GameObject Env;
    GameObject newgimmick;
    Text RemainPanelText;
    /// <summary>
    /// 사용할 수 있는 남은 횟수
    /// </summary>
    [SerializeField]
    private int remaining;
    public GameObject prefab;

    void Awake()
    {
        string trim = transform.name.Remove(0, 8);
        prefab = Resources.Load<GameObject>("Prefabs/" + trim);
        Env = GameObject.Find("Environment");
        RemainPanelText = transform.Find("RemainPanel").Find("Text").GetComponent<Text>();
        RemainPanelText.text = remaining.ToString();
    }
    void Start()
    {


    }
    public void Click()
    {
        print("Showcase Click");

        GameObject newobject = Instantiate(prefab, Env.transform);
        newobject.GetComponent<MoveScript>().SelectObject(newobject,true);
    }
}
