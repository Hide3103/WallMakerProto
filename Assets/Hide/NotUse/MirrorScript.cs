using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    GameObject targetObj;
    Transform targetTrans;
    Vector3 targetPos;

    public Vector3 targetMirrorPos;

    GameObject player;
    PlayerController playerScript;
    public GameObject mirrorPlayer;

    public GameObject enemy;
    public GameObject fieldObj;
    GameObject selectedObj;

    MirrorObject mirrorObjectScript;

    int refrectObj = (int)ObjectType.player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        targetObj = player;
    }

    // Update is called once per frame
    void Update()
    {
        targetTrans = targetObj.GetComponent<Transform>();
        targetPos = targetTrans.position;

        switch (refrectObj)
        {
            case (int)ObjectType.player:
                selectedObj = mirrorPlayer;
                break;
            case (int)ObjectType.enemy:
                selectedObj = enemy;
                break;
            case (int)ObjectType.Object:
                selectedObj = fieldObj;
                break;

            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            //Instantiate(selectedObj, targetMirrorPos, targetTrans.rotation);
            //mirrorObjectScript = selectedObj.GetComponent<MirrorObject>();
            //mirrorObjectScript.MirrorPrefab = this.gameObject;
            playerScript.transform.position = GetTargetMirrorPos();
        }

        UpdateMirrorObjectPos(targetObj);
    }

    void UpdateMirrorObjectPos(GameObject obj)
    {
        float rot = this.transform.rotation.eulerAngles.y;
        float edge1_x = Mathf.Cos(-rot * Mathf.Deg2Rad) * this.transform.localScale.x / 2.0f;
        float edge1_z = Mathf.Sin(-rot * Mathf.Deg2Rad) * this.transform.localScale.y / 2.0f;
        float edge2_x = -edge1_x;
        float edge2_z = -edge1_z;
        Vector3 edge1pos = new Vector3(edge1_x, this.transform.position.y, edge1_z);
        Vector3 edge2pos = new Vector3(edge2_x, this.transform.position.y, edge2_z);


        edge1pos += this.transform.position;
        edge2pos += this.transform.position;

        //Debug.Log("rot : " + rot);
        //Debug.Log("edge1 X:" + edge1pos.x + ", Z:" + edge1pos.z);
        //Debug.Log("edge2 X:" + edge2pos.x + ", Z:" + edge2pos.z);
        //Debug.Log("mirrorPosX : " + this.transform.position.x);
        //Debug.Log("mirrorPosZ : " + this.transform.position.z);

        targetMirrorPos = PerpendicularFootPoint(edge1pos, edge2pos, targetPos);
        Vector3 distance;
        distance = targetMirrorPos - targetPos;

        targetMirrorPos += distance;
        targetMirrorPos.y = targetPos.y;
    }

    public Vector3 GetTargetMirrorPos()
    {
        return targetMirrorPos;
    }

    private Vector3 PerpendicularFootPoint(Vector3 a, Vector3 b, Vector3 p)
    {
        return a + Vector3.Project(p - a, b - a);
    }
}
