using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3DText는 2D sprite와 sorting할 수 없기 때문에 스크립트로 강제적으로 설정해야한다.
/// </summary>
public class SpriteText : MonoBehaviour
{
    public Renderer parentRenderer;

    void Start()
    {
        //Body오브젝트의 SpriteRenderer(Renderer)를 가져와 텍스트의 sortinglayer를 Body의 layer와 똑같이 맞춘다.
        //https://answers.unity.com/questions/620747/render-text-on-sprite-prefab-2d-ios.html
        var parent = transform.parent;
        parentRenderer = parent.transform.Find("Body").GetComponent<Renderer>();
        var renderer = GetComponent<Renderer>();
        renderer.sortingLayerID = parentRenderer.sortingLayerID;
        renderer.sortingOrder = parentRenderer.sortingOrder;
    }
}
