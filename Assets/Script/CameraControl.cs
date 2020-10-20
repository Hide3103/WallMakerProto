using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector3 targetPos;
    public GameObject player;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(playerPos.x, transform.position.y, playerPos.z);
    }
}
