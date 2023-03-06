using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    Player player;
    Transform canvas;

    int kills = 0;

    public static GameMode Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        canvas = GameObject.Find("Canvas").transform;
    }

    public void OnEnemyDied()
    {
        kills++;
        canvas.Find("Kills").GetComponent<Text>().text = "杀敌：" + kills;
    }
    public void SetWeaponText(int weapon)
    {
        Text t = canvas.Find("Weapon").GetComponent<Text>();
        switch (weapon)
        {
            case 0:
                t.text = "手枪";
                break;
            case 1:
                t.text = "霰弹枪";
                break;
            case 2:
                t.text = "自动步枪";
                break;
        }
    }
}
