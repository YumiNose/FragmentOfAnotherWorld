using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 3.5f;  // 移動速度

    SpriteRenderer spriteRenderer;  // 左右反転用

    Rigidbody2D rigidbody2D;  // ジャンプ用

    [SerializeField]
    float jumpForce = 300f; // ジャンプ力

    bool isGround = true;  // 地面に着いているのか

    Rigidbody2D Rigidbody2D;

    [SerializeField]
    bool isMain = true; //操作キャラクター

    [SerializeField]
    GameObject char2;//操作してないキャラクター

    public Sprite chara1; //メインキャラクターのスプライト
    public Sprite chara2;//サブキャラクターのスプライト

    string enemyTag = "Enemy";

    static Vector3 spawnPosition;//復活ポイント

    AudioSource audioSource;

    public AudioClip Jump; //ジャンプの効果音
    public AudioClip FallDeath;//落下したときの効果音
    public AudioClip CheckPoint;//チェックポイントで復活したときの効果音
    public AudioClip Gloa;
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        


        if (ScenesInfo.lastSceneIndex != SceneManager.GetActiveScene().buildIndex || spawnPosition == Vector3.zero)//もしひとつ前のシーンと現在のシーンが違うときだったら（復活じゃないとき）
        {
            spawnPosition = transform.position;//復活する場所
        }
        else//で復活したとき
        {
            
            if(spawnPosition.y >= 0)//チェックポイントが上の時に触れていた時
            {
                if(isMain == true)//上のキャラがメインの時
                {
                    transform.position = spawnPosition; //復活
                }
                else
                {
                    transform.position = new Vector3(spawnPosition.x, -spawnPosition.y,spawnPosition.z); //yを反対にして復活
                }
            }
            else
            {
                if (isMain == false) //下のキャラがメインの時
                {
                    transform.position = spawnPosition; //復活
                }
                else
                {
                    transform.position = new Vector3(spawnPosition.x, -spawnPosition.y, spawnPosition.z); //yを反対にして復活
                }
            }
           

        }

        
    }
    void Update()
    {
        //上のキャラがメインの時
        if (isMain == true)
        {

            Move();

            Fall();

        }//下のキャラがメインの時
        else
        {
            Movesub();
        }

        //Fキーを押したら操作キャラクターを切り替える
        if (Input.GetKeyDown(KeyCode.F))
        {

            //重力保存
            Vector2 gravity = Physics2D.gravity;

            //上のキャラメインのとき
            if (isMain == true)
           {
                //重力を反対にする                
                gravity.y *= -1;
                Physics2D.gravity = gravity;

                isMain = false;               
              
                ColorChange();//スプライト切り替え       

              
                
            }
            else//下のキャラがメイン
            {
                isMain = true;//上のキャラをメインにする

                ColorChange();//スプライト切り替え     


            }
            this.rigidbody2D.velocity = Vector2.zero; //キャラクターにかかる力をゼロにする
        }

    }

    //メインキャラクーの操作
    void Move()
    {
        // キー入力受付
        // 左キーまたはAキーが押されたら-1
        // 右キーまたはDキーが押されたら1
        float move = Input.GetAxis("Horizontal");

        // 左右移動
        transform.Translate(move * this.speed * Time.deltaTime, 0, 0);
        
        // 左右反転
        if (move > 0)
        {
            this.spriteRenderer.flipX = false;
        }
        else if (move < 0)
        {
            this.spriteRenderer.flipX = true;
        }

        // ジャンプ
        if (Input.GetKeyDown("space") && this.isGround)
        {
            // 上に向けて力を加える
            this.rigidbody2D.AddForce(transform.up * this.jumpForce);
           
            // 地面から離れた
            this.isGround = false;
            
        }
    }

    //サブのキャラクターの移動操作
    void Movesub()
    {
        //char2の位置を保存
        Vector3 position = char2.transform.position;
        
        position.y *= -1;

        transform.position = position;

        spriteRenderer.flipX = char2.GetComponent<SpriteRenderer>().flipX;

    }

    //地面に触れているか確認
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //地面に触れているならtrue
            this.isGround = true;
        }

        //敵に当たったら
        if (collision.gameObject.tag == "Enemy")
        {
            //現在のシーンを保存
            ScenesInfo.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // 現在のシーンを再読み込み
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex);
        }
        
       
    }

    //落下
    void Fall()
    {
        // このY座標（bottomY）より下へ落ちたらスタートへ戻す
        float bottomY = Camera.main.transform.position.y
                         - Camera.main.orthographicSize * 2;

        // プレイヤーのY座標がbottomYより低い
        if (gameObject.transform.position.y < bottomY)
        {
            //現在のシーンを保存
            ScenesInfo.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // 現在のシーンを再読み込み
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex);
        }
    }

    //スプライト切り替え関数
    void ColorChange()
    {
        
        if (isMain == true)
        {
            GetComponent<SpriteRenderer>().sprite = chara1;

        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = chara2;
        }
    }

    //チェックポイント関数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            spawnPosition = transform.position;
        }
    }


}
