using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    bool isStartButtonPressed;

    [SerializeField]
    GameObject[] StageNumber;  // ステージ３つの配列

    [SerializeField]
    GameObject Cursor;   // カーソルのオブジェクト

    int cursorPosition;  

    int LRButtonPress;   // 左右のカーソル移動
    int UDButtonPress;   // 上下のカーソル移動

    AudioSource audioSource;
    public AudioClip Action;


    // ステージ１を押したときに呼ぶ関数
    public void OnButton1Pressed()
    {
        if (!this.isStartButtonPressed)
        {
            this.isStartButtonPressed = true;

            audioSource.PlayOneShot(Action);

            // 統合時に要変更
            FadeManager.Instance.LoadScene("Stage1M", 1.0f);
        }
    }

    // ステージ２を押したときに呼ぶ関数
    public void OnButton2Pressed()
    {
        if (!this.isStartButtonPressed)
        {
            this.isStartButtonPressed = true;

            audioSource.PlayOneShot(Action);

            // 統合時に要変更
            FadeManager.Instance.LoadScene("Stage2M", 1.0f);
        }
    }

    // ステージ３を押したときに呼ぶ関数
    public void OnButton3Pressed()
    {
        if (!this.isStartButtonPressed)
        {
            this.isStartButtonPressed = true;

            audioSource.PlayOneShot(Action);

            // 統合時に要変更
            SceneManager.LoadScene("Stage3M");
        }
    }

    // タイトル画面に戻る を押したときに呼ぶ関数
    public void OnButtonTitlePressed()
    {
        if (!this.isStartButtonPressed)
        {
            this.isStartButtonPressed = true;

            audioSource.PlayOneShot(Action);

            // SceneManager.LoadScene("TitleScene");
            FadeManager.Instance.LoadScene("TitleScene", 1.0f);
        }
    }


    private void Start()
    {
        Cursor.transform.position = StageNumber[cursorPosition].transform.position;

        // Audioのコンポーネント取得
        this.audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float H = Input.GetAxis("Horizontal");
        if(H > 0) // 右
        {
            if(LRButtonPress < 0)
            {
                LRButtonPress = 0;
            }
            LRButtonPress++;
            
        }
        else if(H < 0) // 左
        {
            if (LRButtonPress > 0)
            {
                LRButtonPress = 0;
            }
            LRButtonPress--;
        }
        else
            LRButtonPress = 0;


        float V = Input.GetAxis("Vertical");
        if (V > 0) // 上
        {
            if (UDButtonPress < 0)
            {
                UDButtonPress = 0;
            }
            UDButtonPress++;

        }
        else if (V < 0) // 下
        {
            if (UDButtonPress > 0)
            {
                UDButtonPress = 0;
            }
            UDButtonPress--;
        }
        else
            UDButtonPress = 0;



        // キーボードの矢印キーとコントローラーのスティックで右にいく
        if (Input.GetKeyDown(KeyCode.RightArrow) || LRButtonPress ==1)
        {
            cursorPosition++;
        }
        // キーボードの矢印キーとコントローラーのスティックで左にいく
        if (Input.GetKeyDown(KeyCode.LeftArrow) || LRButtonPress == -1)
        {
            cursorPosition--;
        }
        // キーボードの矢印キーとコントローラーのスティックで上にいく
        if (Input.GetKeyDown(KeyCode.UpArrow) || UDButtonPress == 1)
        {
            if(cursorPosition == 3)
                cursorPosition = 1;
        }
        // キーボードの矢印キーとコントローラーのスティックで下にいく
        if (Input.GetKeyDown(KeyCode.DownArrow) || UDButtonPress == -1)
        {
            cursorPosition = 3;
        }

        cursorPosition = Mathf.Clamp(cursorPosition, 0, StageNumber.Length - 1);

        // カーソルの位置を取得
        Cursor.transform.position = StageNumber[cursorPosition].transform.position;


        // ステージセレクトで決定ボタンを押す
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            StageNumber[cursorPosition].GetComponent<Button>().onClick.Invoke();
        }
        
    }

}
