﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 16:9로 해상도 고정
/// </summary>
public class ResolutionMethod : MonoBehaviour
{

    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;

        float scaleheight = ((float)Screen.width / Screen.height) / ((float)18.5 / 9); //가로, 세로

        float scalewidth = 1f / scaleheight;
        if(scaleheight<1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;

        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }
}
