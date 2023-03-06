using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3;

    public float maxHp = 20;

    Vector3 input;
    bool dead = false;

    float hp;
    MeshRenderer render;

    Weapon weapon;

    GameMode gameMode;

    void Start()
    {
        hp = maxHp;
        render = GetComponent<MeshRenderer>();
        weapon = GetComponent<Weapon>();

        gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();
    }

    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Debug.Log(input);

        bool fireKeyDown = Input.GetKeyDown(KeyCode.J);
        bool fireKeyPressed = Input.GetKey(KeyCode.J);
        bool changeWeapon = Input.GetKeyDown(KeyCode.Q);

        if (!dead)
        {
            Move();
            weapon.Fire(fireKeyDown, fireKeyPressed);

            if (changeWeapon)
            {
                ChangeWeapon();
            }
        }
    }

    void Move()
    {
        input = input.normalized;
        transform.position += input * speed * Time.deltaTime;
        if (input.magnitude > 0.1f)
        {
            transform.forward = input;
        }

        Vector3 temp = transform.position;
        const float BORDER = 20;
        if (temp.z > BORDER) { temp.z = BORDER; }
        if (temp.z < -BORDER) { temp.z = -BORDER; }
        if (temp.x > BORDER) { temp.x = BORDER; }
        if (temp.x < -BORDER) { temp.x = -BORDER; }
        transform.position = temp;
    }

    private void ChangeWeapon()
    {
        int w = weapon.Change();
        gameMode.SetWeaponText(w);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            if (hp <= 0)
            {
                return;
            }
            hp--;
            render.material.color = Color.Lerp(Color.white, Color.red, 1 - hp / maxHp);
            if (hp <= 0)
            {
                dead = true;
            }
        }
    }
}
