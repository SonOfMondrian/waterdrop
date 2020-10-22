using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPanel : MonoBehaviour
{

    GameObject ob;
    void Awake()
    {
        ob = gameObject;
        
    }
    void Start()
    {
        ob.SetActive(false);
    }

    /// <summary>
    /// 스테이지 클리어되서 팝업이 띄워진다.
    /// </summary>
    public void Popup()
    {
        ob.SetActive(true);
        GetComponent<Animator>().SetTrigger("Clear");
        
    }

    /// <summary>
    /// 다음 스테이지 버튼 클릭
    /// </summary>
    public void ClickNextButton()
    {
        //구현중
    }

    /// <summary>
    /// 스테이지 버튼 클릭
    /// </summary>
    public void ClickStageButton()
    {
        SceneManager.LoadScene("Stage");

    }

}
