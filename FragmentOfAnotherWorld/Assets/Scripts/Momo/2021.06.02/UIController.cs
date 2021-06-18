using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public int enemy = 1;
    public GameObject enemyText;

    void Start()
    {
        this.enemyText = GameObject.Find("Enemy");
        
    }

    void Update()
    {
        enemyText.GetComponent<Text>().text = "のこりのてきの数  " + enemy.ToString("D4");
    }

    public void AddEnemy()
    {
        this.enemy--;
    }
}
