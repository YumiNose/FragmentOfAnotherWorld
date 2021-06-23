using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    bool isStartButtonPressed;

    [SerializeField]
    GameObject[] StageNumber;  // 選択２つの配列

    [SerializeField]
    GameObject Cursor;   // カーソルのオブジェクト

    int cursorPosition;  // カーソルポジション

    int LRButtonPress;   // 左右のカーソル移動

    public static int stageIndex;

    AudioSource audioSource;
    public AudioClip Action;


    // ステージ選択に戻るを押したときに呼ぶ関数
    public void OnReturnButtonPressed()
    {
        if (!this.isStartButtonPressed)
        {
            this.isStartButtonPressed = true;
            audioSource.PlayOneShot(Action);

            //SceneManager.LoadScene("SelectScene");
            FadeManager.Instance.LoadScene("SelectScene", 1.0f);
        }
    }

    // 次のステージへを押したときに呼ぶ関数
    public void OnNextButtonPressed()
    {
        if (!this.isStartButtonPressed)
        {
            this.isStartButtonPressed = true;
            audioSource.PlayOneShot(Action);

            // 要変更
            SceneManager.LoadScene("Stage2M");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.transform.position = StageNumber[cursorPosition].transform.position;

        // Audioのコンポーネント取得
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float H = Input.GetAxis("Horizontal");
        if (H > 0) // 右
        {
            if (LRButtonPress < 0)
            {
                LRButtonPress = 0;
            }
            LRButtonPress++;

        }
        else if (H < 0) // 左
        {
            if (LRButtonPress > 0)
            {
                LRButtonPress = 0;
            }
            LRButtonPress--;
        }
        else
            LRButtonPress = 0;

        // キーボードの矢印キーとコントローラーのスティックで右にいく
        if (Input.GetKeyDown(KeyCode.RightArrow) || LRButtonPress == 1)
        {
            cursorPosition++;
        }
        // キーボードの矢印キーとコントローラーのスティックで左にいく
        if (Input.GetKeyDown(KeyCode.LeftArrow) || LRButtonPress == -1)
        {
            cursorPosition--;
        }

        cursorPosition = Mathf.Clamp(cursorPosition, 0, StageNumber.Length - 1);

        // カーソルの位置を取得
        Cursor.transform.position = StageNumber[cursorPosition].transform.position;


        // リザルトで決定ボタンを押す
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            StageNumber[cursorPosition].GetComponent<Button>().onClick.Invoke();
        }
    }
}
