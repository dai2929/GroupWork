using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_GameChecker : MonoBehaviour
{
    public static string gameState ="playing" ;    //ゲームの状態

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameState == "gameclear") return;

        //Enemyタグを持つオブジェクトがGameOverPointに触れた場合
        if (other.CompareTag("Enemy"))
        {
            //ゲームオーバー処理
            gameState = "gameover";
            Debug.Log("gameState: " + a_GameChecker.gameState);  // 状態を確認する
        }
    }

    //EnemyがGameOverPointを通過と同時に消滅
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
