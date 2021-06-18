using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]  GameObject pausePanel;

    AudioSource audioSource;
    public AudioClip Poz; // ポーズした時の効果音
    public AudioClip Action; // 選択を決定したときに鳴らす音


    [SerializeField]
    GameObject[] StageNumber;  // ステージ３つの配列

    [SerializeField]
    GameObject Cursor;   // カーソルのオブジェクト

    int cursorPosition;

    int UDButtonPress;   // 上下のカーソル移動



    [SerializeField]
    bool isPause = false;  // ポーズ画面

    bool isStartButtonPressed;

    // つづけるを押したときに呼ぶ関数
    public void OnButtonContinuePressed()
    {
        if (!this.isStartButtonPressed)
        {
            this.isStartButtonPressed = true;

            audioSource.PlayOneShot(Action);

            Resume();

        }
    }

    // やりなおすを押したときに呼ぶ関数
    public void OnButtonRedoPressed()
    {
        if (!this.isStartButtonPressed)
        {
            this.isStartButtonPressed = true;

            audioSource.PlayOneShot(Action);

            // 統合時に要変更
            FadeManager.Instance.LoadScene("Stage1M", 1.0f);
        }
    }

    // ステージ選択へを押したときに呼ぶ関数
    public void OnButtonSelectPressed()
    {
        if (!this.isStartButtonPressed)
        {
            this.isStartButtonPressed = true;

            audioSource.PlayOneShot(Action);

            // 統合時に要変更
            FadeManager.Instance.LoadScene("SelectScene", 1.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        if(isPause == false)
        {
            return;
        }

        Cursor.transform.position = StageNumber[cursorPosition].transform.position;

        // Audioのコンポーネント取得
        this.audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        

        if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Pause"))
        {
            pause();
            audioSource.PlayOneShot(Poz); // ポーズを押したときに鳴らす音

            cursor();

        }




    }


    void pause()
    {
        Time.timeScale = 0;  // 時間停止
        //pausePanel.SetActive(true);
        isPause = true;
    }

    void Resume()
    {
        Time.timeScale = 1;  // 再開
        //pausePanel.SetActive(false);
        isPause = false;
    }

    void cursor()
    {
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

        // キーボードの矢印キーとコントローラーのスティックで上にいく
        if (Input.GetKeyDown(KeyCode.UpArrow) || UDButtonPress == 1)
        {
            //if (cursorPosition == 3)
                //cursorPosition = 1;
            cursorPosition--;
        }
        // キーボードの矢印キーとコントローラーのスティックで下にいく
        if (Input.GetKeyDown(KeyCode.DownArrow) || UDButtonPress == -1)
        {
            //cursorPosition = 3;
            cursorPosition++;
        }

        cursorPosition = Mathf.Clamp(cursorPosition, 0, StageNumber.Length - 1);

        // カーソルの位置を取得
        Cursor.transform.position = StageNumber[cursorPosition].transform.position;


        // ステージセレクトで決定ボタンを押す
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            StageNumber[cursorPosition].GetComponent<Button>().onClick.Invoke();
        }
    }
}
