using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseGenerator : MonoBehaviour
{
    public GameObject TreeShowcasePref;
    public GameObject MetalShowcasePref;
    public GameObject AmpShowcasePref;
    public GameObject GunShowcasePref;

    void Awake()
    {
        TreeShowcasePref = Resources.Load<GameObject>("Prefabs/UIs/ShowcaseTree");
        MetalShowcasePref = Resources.Load<GameObject>("Prefabs/UIs/ShowcaseMetal");
        AmpShowcasePref = Resources.Load<GameObject>("Prefabs/UIs/ShowcaseAmp");
        GunShowcasePref = Resources.Load<GameObject>("Prefabs/UIs/ShowcaseGun");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
