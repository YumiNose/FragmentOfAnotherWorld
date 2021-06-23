using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageController : MonoBehaviour
{
    void Start()
    {
        //シーンを切り替えてもオブジェクトを削除しないようにする

    }

    void Update()
    {
        
    }

    /// <summary>
    /// 次のステージに進む処理
    /// </summary>
    public void changeScene()
    {
        //現在のシーンの名前を取得
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Result")
        {
            SceneManager.LoadScene("Stage2M");
        }


        //現在のシーンのインデックス番号を取得
        int nowSceneIndexNumber = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(++nowSceneIndexNumber);
    }
}
