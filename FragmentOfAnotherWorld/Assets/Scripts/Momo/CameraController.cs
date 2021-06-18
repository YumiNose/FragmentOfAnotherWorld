using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject target;　//ターゲット設定

    [SerializeField]
    float offset = 5f;　//カメラの移動スピード

    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 position = transform.position;

        bool flipX = target.GetComponent<SpriteRenderer>().flipX;

        if (flipX == false)//右に移動してるとき
        {
           
            position.x = target.transform.position.x + offset;
            
        }
        else//左に移動してるとき
        {
            position.x = target.transform.position.x - offset;
        }

       

        　　　　　　　　　　　　　　　　　　//現在地　　　　目的地　　
            transform.position = Vector3.Lerp(transform.position, position, 0.07f);
     
       
           
    }
}
