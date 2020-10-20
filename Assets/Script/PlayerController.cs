using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ObjectType
{
    player,
    enemy,
    Object
}


public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    float moveSpeed = 3.0f;

    float inputHorizontal_L = 0.0f;
    float inputVertical_L = 0.0f;
    float inputHorizontal_R = 0.0f;
    float inputVertical_R = 0.0f;
    bool mirrorFlg;

    public Vector3 offset = new Vector3();
    public GameObject mirrorPrefab;
    Vector3 mirrorPos;
    public GameObject skeltonMirror;

    Transform cameraTransform;

    public int selectObj = (int)ObjectType.player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal_L = Input.GetAxisRaw("HorizontalL");
        inputVertical_L = Input.GetAxisRaw("VerticalL");
        inputHorizontal_R = Input.GetAxisRaw("HorizontalR");
        inputVertical_R = Input.GetAxisRaw("VerticalR");
    }

    private void FixedUpdate()
    {
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveForward = cameraForward * inputVertical_L + Camera.main.transform.right * inputHorizontal_L;

        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (inputHorizontal_R != 0 || inputVertical_R != 0)
        {
            Vector3 direction = cameraForward * inputVertical_R + cameraTransform.right * inputHorizontal_R;
            transform.localRotation = Quaternion.LookRotation(direction);
        }
        else
        {
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
        }

        float RTrigger = Input.GetAxis("RTrigger");
        if(RTrigger > 0.0f)
        {
            mirrorFlg = true;
            skeltonMirror.SetActive(true);
        }
        else
        {
            skeltonMirror.SetActive(false);
            if(mirrorFlg == true)
            {
                GenerateMirror();
                mirrorFlg = false;
            }
        }
    }

    void GenerateMirror()
    {
        Vector3 mirrorPosition = transform.position +
        transform.up * offset.y +
        transform.right * offset.x +
        transform.forward * offset.z;

        Instantiate(mirrorPrefab, mirrorPosition, transform.rotation);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
