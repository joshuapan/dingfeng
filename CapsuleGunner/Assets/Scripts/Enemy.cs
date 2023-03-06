using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject prefabBullet;
    public GameObject prefabBoomEffect;

    public float speed = 2;
    public float fireTime = 0.1f;
    public float maxHp = 1;

    Vector3 input;
    bool fire = false;

    float lastFireTime;

    Transform player;
    float hp;
    bool dead = false;

    GameMode gameMode;
    Weapon weapon;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();
        weapon = GetComponent<Weapon>();
    }

    void Update()
    {
        Move();
        Fire();
    }

    void Move()
    {
        input = player.position - transform.position;
        input = input.normalized;

        transform.position += input * speed * Time.deltaTime;
        if (input.magnitude > 0.1f)
        {
            transform.forward = input;
        }
    }

    void Fire()
    {
        if (lastFireTime + fireTime > Time.time)
        {
            return;
        }
        lastFireTime = Time.time;

        GameObject bullet = Instantiate(prefabBullet, null);
        bullet.transform.position = transform.position + transform.forward * 1.0f;
        bullet.transform.forward = transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            hp--;
            if (hp <= 0)
            {
                dead = true;
                Instantiate(prefabBoomEffect, transform.position, transform.rotation);
                Destroy(gameObject);

                gameMode.OnEnemyDied();
            }
        }
    }
}
