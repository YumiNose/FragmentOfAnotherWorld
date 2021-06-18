using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [Header("移動経路")] public GameObject[] movePoint;
    [Header("速さ")] public float speed = 1.0f;

    float t;
    bool isLeft;
    void Start()
    {
        transform.position = movePoint[0].transform.position;

    }

    private void Update()
    {
        transform.position = Vector3.Lerp(movePoint[0].transform.position, movePoint[1].transform.position, t);

        if(isLeft == true)
        {
            t -= speed * Time.deltaTime;
            if( t <= 0)
            {
                isLeft = false;
            }
        }
        else
        {
            t += speed * Time.deltaTime;
            if (t >= 1)
            {
                isLeft = true;
            }
        }
    }
}
