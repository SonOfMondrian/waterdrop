using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    /// <summary>
    /// 스테이지 씬에서 스테이지 버튼을 눌렀을때 호출
    /// </summary>
    public void ClickStageNumButton()
    {
        //현재 버튼의 이름을 인자로 보내서 그 이름에 맞는 스테이지 오브젝트를 리턴함 ex) 1-1 -> 1월드 1스테이지 오브젝트 리턴
        GameObject stage = StageManager.instance.ClickStage(transform.name);
        //StopCoroutine(StageManager.instance.CameraMoving());

        if(stage!=null)
            SceneManager.LoadScene("ingame");
    }
    /// <summary>
    /// 왼쪽, 오른쪽 버튼을 클릭시 호출
    /// </summary>
    public void ClickArrowButton(int v)
    {
        StageManager.instance.ClickArrow(v);
    }
}
