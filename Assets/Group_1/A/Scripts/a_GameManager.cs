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

    //追加UI(発射レート、弾速、使用Bullet)
    public TextMeshProUGUI bulletSpeedText;
    public TextMeshProUGUI fireRateText;
    public TextMeshProUGUI bulletTypeText;
    // a_Shooter インスタンスを参照
    public a_Shooter shooter;

    public static a_GameManager Instance { get; private set; }
    List<a_BaseEnemy> enemies = new List<a_BaseEnemy>();

    //SE
    public AudioClip gameClearSound;
    public AudioClip gameOverSound;
    AudioSource audioSource;    //AudioSourceの参照
    bool hasPlayedClearSound = false; // ゲームクリア音が再生済みか
    bool hasPlayedOverSound = false; // ゲームオーバー音が再生済みか


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterEnemy(a_BaseEnemy enemy)
    {
        enemies.Add(enemy);
    }

    public void UnregisterEnemy(a_BaseEnemy enemy)
    {
        enemies.Remove(enemy);
    }

    public void ReverseAllEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.ReverseDirection();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //ステータスをplayingに
        a_GameChecker.gameState = "playing";

        //ボタン(パネル)を非表示
        panel.SetActive(false);

        //テキストを非表示する
        Invoke("HideText", 1f);

        //AudioSourceの取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //gameState監視用
        Debug.Log($"Current gameState: {a_GameChecker.gameState}");

        //GameChecker.gameStateがplayingの時だけカウントアップ
        if (a_GameChecker.gameState == "playing")
        {
            //時間のカウントアップ
            gameTime += Time.deltaTime;
            timeText.text = "TIME:" + Mathf.CeilToInt(gameTime).ToString();
        }

        // 弾の状態を表示
        UpdateBulletInfo();

        //ゲームクリア
        if (a_GameChecker.gameState == "gameclear" && !hasPlayedClearSound)
        {
            Debug.Log("ゲームクリア");
            ShowStatus("GAME CLEAR");

            //ゲームクリアSEを再生
            if (audioSource != null && gameClearSound != null)
            {
                audioSource.PlayOneShot(gameClearSound);
            }

            hasPlayedClearSound = true; // 再生済みフラグを設定
        }
        //ゲームオーバー
        else if (a_GameChecker.gameState == "gameover" && !hasPlayedOverSound)
        {
            Debug.Log("ゲームオーバー処理");
            ShowStatus("GAME OVER");

            //ゲームオーバーSEを再生
            if (audioSource != null && gameOverSound != null)
            {
                audioSource.PlayOneShot(gameOverSound);
            }

            hasPlayedOverSound = true; // 再生済みフラグを設定
        }
    }

    // 弾の状態を表示するメソッド
    void UpdateBulletInfo()
    {
        if (shooter != null) 
        {
            bulletSpeedText.text = "弾速: " + shooter.currentBulletSpeed.ToString();
            fireRateText.text = "連射速度: " + shooter.currentFireRate.ToString();
            bulletTypeText.text = "威力: " + (shooter.CurrentBulletPrefab == shooter.normalBulletPrefab ? "1" : "2");
        }
    }

    //ゲームステータスを表示
    void ShowStatus(string status)
    {
        statusText.GetComponent<TextMeshProUGUI>().text = status;
        statusText.SetActive(true);
        panel.SetActive(true);

        //繰り返しを避ける為
        //a_GameChecker.gameState = "gameend";
    }

    //テキストを非表示
    void HideText()
    {
        statusText.SetActive(false);
    }

}
