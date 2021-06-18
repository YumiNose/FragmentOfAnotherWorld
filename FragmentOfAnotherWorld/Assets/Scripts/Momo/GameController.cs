using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject ClearText;
    public AudioClip Goal;//ゴールした時の効果音
    AudioSource audioSource;


    void Start()
    {
        //テキストを非表示にする
        ClearText.SetActive(false);

        //コンポーネント取得
        this.audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    
    //クリア判定関数
    public void OnTriggerEnter2D(Collider2D collider)
    {
       
        //ゴールエリアにプレイヤーが入ったら
        if (collider.gameObject.tag == "Player")
        {
            //テキストを表示させる
            ClearText.SetActive(true);

            audioSource.PlayOneShot(Goal);

            //SceneManager.LoadScene("ResultScene", 1.0f);
            FadeManager.Instance.LoadScene("ResultScene", 2.0f);
        }

    }
}