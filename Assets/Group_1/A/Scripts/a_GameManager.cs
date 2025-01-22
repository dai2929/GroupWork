using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class a_GameManager : MonoBehaviour
{
    public GameObject statusText;   //必要に応じて状態を表示させる

    public TextMeshProUGUI timeText;    //タイム
    public float gameTime = 0f; //基準時間

    public GameObject panel;    //パネル
    public GameObject restartButton;    //リスタートボタン
    public GameObject stageSelect;  //ステージセレクト

    // Start is called before the first frame update
    void Start()
    {
        //ステータスをplayingに
        a_GameChecker.gameState = "playing";

        //ボタン(パネル)を非表示
        panel.SetActive(false);

        //テキストを非表示する
        Invoke("HideText", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //GameChecker.gameStateがplayingの時だけカウントアップ
        if (a_GameChecker.gameState == "playing")
        {
            Debug.Log("gameState: " + a_GameChecker.gameState);  // 状態を確認する
            //時間のカウントアップ
            gameTime += Time.deltaTime;
            timeText.text = "TIME:" + Mathf.CeilToInt(gameTime).ToString();
        }

        //ゲームクリア
        if (a_GameChecker.gameState == "gameclear")
        {
            Debug.Log("ゲームクリア");
            ShowStatus("GAME CLEAR");
        }
        //ゲームオーバー
        else if (a_GameChecker.gameState == "gameover")
        {
            Debug.Log("ゲームオーバー処理");
            ShowStatus("GAME OVER");
        }
    }

    //ゲームステータスを表示
    void ShowStatus(string status)
    {
        statusText.GetComponent<TextMeshProUGUI>().text = status;
        statusText.SetActive(true);
        panel.SetActive(true);

        //繰り返しを避ける為
        a_GameChecker.gameState = "gameend";
    }

    //テキストを非表示
    void HideText()
    {
        statusText.SetActive(false);
    }

}
