using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMain : MonoBehaviour
{
    void Start()
    {
        //스테이지 씬 상태에서 시작시 자동으로 메인씬으로 이동하는 코드(스테이지 매니저가 없기때문에 메인씬에서 가져와야하기 때문)
        if (GameObject.Find("StageManager") == null)
            SceneManager.LoadScene("Main");
    }
}
