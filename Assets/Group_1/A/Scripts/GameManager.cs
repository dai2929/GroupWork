using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string gameState;    //ゲームの状態
    public GameObject statusText;   //必要に応じて状態を表示させる

    public TextMeshProUGUI timeText;    //タイム
    public float gameTime = 0f; //基準時間

    public GameObject panel;    //パネル
    public GameObject restartButton;    //リスタートボタン
    public GameObject stageSelect;  //ステージセレクト

    public GameObject deadPoint;    //GameOverPoint
    public GameObject warningPoint; //警告エリア


    // Start is called before the first frame update
    void Start()
    {
        //ステータスをplayingに
        gameState = "playing";

        //ボタン(パネル)を非表示
        panel.SetActive(false);

        //テキストを非表示する
        Invoke("HideText", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //gameStateがplayingの時だけカウントアップ
        if (gameState == "playing")
        {
            //時間のカウントアップ
            gameTime += Time.deltaTime;
            timeText.text = "TIME:" + Mathf.CeilToInt(gameTime).ToString();
        }
        else
        {
            return;
        }

        //ゲームクリア
        if (gameState == "gameclear")
        {
            //statusTextをゲームクリアに
            statusText.GetComponent<TextMeshProUGUI>().text = "GAME CLEAR";
            //statusTextとパネルを表示
            statusText.SetActive(true);
            panel.SetActive(true);

            //繰り返しを避ける為
            gameState = "gameend";  
        }
        else if (gameState == "gameover")
        {
            //statusTextをゲームオーバーに
            statusText.GetComponent<TextMeshProUGUI>().text = "GAME OVER";
            //statusTextとパネルを表示
            statusText.SetActive(true);
            panel.SetActive(true);

            //繰り返しを避ける為
            gameState = "gameend";
        }
        else
        {
            return;
        }
    }

    //テキストを非表示
    void HideText()
    {
        statusText.SetActive(false);
    }

    //DeadPointにロボが到達
    private void OnTriggerEnter(Collider other)
    {
        
    }

}
