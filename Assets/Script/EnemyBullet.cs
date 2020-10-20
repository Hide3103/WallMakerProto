using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // bullet prefab
    public GameObject bullet;

    // 弾丸発射点
    public Transform enemymuzzle;

    float m_bulletSpeed;
    float m_fireTime; 

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
        GameObject bullets = Instantiate(bullet) as GameObject;

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
}
