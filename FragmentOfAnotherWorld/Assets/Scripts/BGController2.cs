﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController2 : MonoBehaviour
{
    /*
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = -7.37 * (0.02f);
    }

    // 背景のX座標　=　キャラのX座標　* (-0.02f) // 背景が動く速さ
    */

    // スクロールするスピード
    public float speed = 0.1f;

    void Update()
    {
        // 時間によってYの値が0から1に変化していく。1になったら0に戻り、繰り返す。
        float y = Mathf.Repeat(Time.time * speed, 1);

        // Yの値がずれていくオフセットを作成
        Vector2 offset = new Vector2(0, y);

        // マテリアルにオフセットを設定する
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
