using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{

    public float llfeTime = 1.0f;　//ビームの寿命
    void Start()
    {
        Destroy(gameObject,llfeTime);
    }

    void Update()
    {
        var _pos = transform.position;
        transform.position = new Vector3(_pos.x + 20.0f * Time.deltaTime, _pos.y, _pos.z);　  //ビーム生成位置
    }

   /// <summary>
   /// 敵に当たったらビームを消す
   /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
