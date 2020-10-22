using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public void ClickStageNumButton()
    {
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
