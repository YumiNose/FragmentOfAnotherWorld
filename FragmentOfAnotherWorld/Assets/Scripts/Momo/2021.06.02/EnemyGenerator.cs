using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    void Start()
    {
      
    }

    void Update()
    {
     ;
    }

    public void GenEnemy()
    {
        Instantiate(enemyPrefab,transform.position,Quaternion.identity);
    }
}
