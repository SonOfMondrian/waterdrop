﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickStageNumButton()
    {
        StageManager.instance.GetStage(transform.name);

        SceneManager.LoadScene("ingame");
    }
}
