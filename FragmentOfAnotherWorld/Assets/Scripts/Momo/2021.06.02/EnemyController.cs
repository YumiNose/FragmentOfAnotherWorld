using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour
{
    public ParticleSystem hitEffect;    //敵が死んだときのパーティクル

   

    void Start()
    {
       
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Beam")
        {
            //エフェクト再生
            var _clone = Instantiate(hitEffect);   
            _clone.transform.position = transform.position;
            _clone.Play();

            //ビームが当たったら敵を消す
            Destroy(gameObject);
        }
    }
}
