using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public void ClickStageNumButton()
    {
        StageManager.instance.GetStage(transform.name);
        StageManager.instance.IsMoving = false;
        //StopCoroutine(StageManager.instance.CameraMoving());
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
