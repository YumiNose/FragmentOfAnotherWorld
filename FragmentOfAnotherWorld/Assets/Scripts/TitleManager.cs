using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip Action;

    bool isStartButtonPressed;

    // スタートボタンを押したときに呼ぶ関数
    public void OnStartButtonPressed()
    {
        if (!this.isStartButtonPressed)
        {
            this.isStartButtonPressed = true;

            //SceneManager.LoadScene("SelectScene");
            
        }
    }

    // Quitボタンを押した時に呼ぶ関数
    public void OnQuitButtonPressed()
    {
#if UNITY_EDITOR
        // UnityEditorでゲームを実行している場合の終了方法
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        // ビルド後のゲームを終了する方法
        Application.Quit();
#endif
    }

     void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            audioSource.PlayOneShot(Action);
            //SceneManager.LoadScene("SelectScene");
            FadeManager.Instance.LoadScene("SelectScene", 1.0f);
        }
    }

    


}
