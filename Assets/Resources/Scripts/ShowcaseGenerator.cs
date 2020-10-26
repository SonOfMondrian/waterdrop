using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseGenerator : MonoBehaviour
{
    List<GameObject> GimmickPrefs;
    public GameObject TreeShowcasePref;
    public GameObject MetalShowcasePref;
    public GameObject AmpShowcasePref;
    public GameObject GunShowcasePref;
    public GameObject GravityShowcasePref;

    void Awake()
    {
        GimmickPrefs = new List<GameObject>();

        //기믹 만들때 마다 넣어주자!
        GimmickPrefs.Add(Resources.Load<GameObject>("Prefabs/UIs/ShowcaseTree"));
        GimmickPrefs.Add(Resources.Load<GameObject>("Prefabs/UIs/ShowcaseMetal"));
        GimmickPrefs.Add(Resources.Load<GameObject>("Prefabs/UIs/ShowcaseAmp"));
        GimmickPrefs.Add(Resources.Load<GameObject>("Prefabs/UIs/ShowcaseGun"));
        GimmickPrefs.Add(Resources.Load<GameObject>("Prefabs/UIs/ShowcaseGravity"));

    }

    public void SetupGimmicks(int[] gimmicks)
    {

        for(int i=0;i<gimmicks.Length;i++)
        {
            if(gimmicks[i]>0)
            {
                print("SetupGimmicks Method Call");
                GameObject newgimmick = Instantiate(GimmickPrefs[i], transform);
                newgimmick.GetComponent<GimmickPanel>().SetRemaining(gimmicks[i]);
            }
        }
    }
}
