using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerBullet : MonoBehaviour
{
    public float m_Attack = 2.0f;

    public float m_BulletHp = 2.0f;

    public float m_ArriveTime = 5.0f;

    bool flg_reverse;

    //float m_bulletSpeed;

    Rigidbody rb;

    private Vector3 lastVelocity;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        m_ArriveTime -= delta;

        if (m_ArriveTime < 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

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
}
