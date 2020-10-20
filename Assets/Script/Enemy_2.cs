using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    public float hp = 2.0f;
    public float moveSpeed = 10.0f;

    // 弾のプレハブ
    public GameObject powerBulletPrefab;

    float moveInterval = 5.0f;
    float delta = 0.0f;

    // 弾丸発射点
    public Transform enemymuzzle;

    float m_bulletSpeed;
    float m_fireTime;

    public Transform playerTrans;
    public float EyeLange = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Initailize();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fireTime >= 5.0f)
        {
            Fire();
        }
        else
        {
            Move();

        }

        Reload();

    }

    public void Initailize()
    {
        m_bulletSpeed = 500.0f;
        m_fireTime = 0.0f;
    }

    public void Fire()
    {
        // 弾丸の複製
        GameObject bullets = Instantiate(powerBulletPrefab) as GameObject;

        Vector3 force;

        force = this.gameObject.transform.forward * m_bulletSpeed;

        // Rigidbodyに力を加えて発射
        bullets.GetComponent<Rigidbody>().AddForce(force);

        // 弾丸の位置を調整
        bullets.transform.position = enemymuzzle.position;

        bullets.transform.rotation = enemymuzzle.rotation;

        m_fireTime = 0.0f;
    }

    public void Reload()
    {
        m_fireTime += Time.deltaTime;
    }

    public void Move()
    {
        float distance = Vector3.Distance(this.transform.position, playerTrans.position);
        Debug.Log(distance);


        if (EyeLange > distance)
        {
            float step = moveSpeed * Time.deltaTime;

            this.transform.position = Vector3.MoveTowards(transform.position, playerTrans.position, step);
        }
    }
}
