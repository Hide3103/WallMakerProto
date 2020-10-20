using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{ 
    bool flg_reverse;

    //float m_bulletSpeed;

    Rigidbody rb;

    private Vector3 lastVelocity;

    //当たり判定
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Mirror")
        {
            flg_reverse = true;
            Vector3 reflectVec = Vector3.Reflect(this.lastVelocity, collision.contacts[0].normal);
            this.rb.velocity = reflectVec;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.lastVelocity = this.rb.velocity;
    }

    public void Initialize()
    {
        //m_bulletSpeed = 50.0f;
        flg_reverse = false;
    }
}
