using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorObject : MonoBehaviour
{
    public GameObject MirrorPrefab;
    MirrorScript mirrorScript;

    // Start is called before the first frame update
    void Start()
    {
        mirrorScript = MirrorPrefab.GetComponent<MirrorScript>();   
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = mirrorScript.GetTargetMirrorPos();

    }

    public void UpdateTrans()
    {
    }
}
