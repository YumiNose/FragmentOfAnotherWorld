using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem pObject;  // パーティクルシステム

    //ここでパーティクルが停止される時間を指定
    float particleDelayTime = 0.2f;

    [SerializeField]
    public GameObject StarAPrefab;

    [SerializeField]
    GameObject wave;



    // Start is called before the first frame update
    void Start()
    {
        //this.wave = GameObject.Find("Wave");
        
    }

    void Awake()
    {
        // 非アクティブ状態
        pObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) )//&& pObject.isStopped)
        {
            // アクティブ状態
            pObject.gameObject.SetActive(true);

            pObject.Simulate(1.0f, true, false); //追記
            pObject.Play(); //追記
            

            StartCoroutine(Delay(particleDelayTime, () => 
            {
                // 非アクティブ状態
                pObject.gameObject.SetActive(false);
                

            }));

            //for Debug
            GameObject obj = Instantiate(StarAPrefab, wave.transform.position, transform.rotation);
            obj.transform.parent = wave.transform;
            Debug.Log("Instantiate");
        }

    }


    IEnumerator Delay(float waitTime, UnityAction action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
