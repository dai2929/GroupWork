using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  //敵のプレハブ
    public Transform spawnPoint;    //出現位置
    public float spawnRangeX = 3f;  //X座標のランダム範囲

    public float[] enemyChangeIntervals;    //各敵への切り替え時間
    int currentEnemyIndex = 0;  //現在出現させる敵のインデックス
    float elapsedTime = 0f; //経過時間

    public float spawnInterval;
    public float initialSpawnInterval = 3f;    //出現間隔
    public float minSpawnInterval = 0.1f;   //最小の出現間隔
    public float spawnIntervalDecreseRate = 0.05f;  //減少する間隔(秒ごと)

    // Start is called before the first frame update
    void Start()
    {
        ////一定間隔で敵を出現させる
        //InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);

        spawnInterval = initialSpawnInterval;   //初期値を設定
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {

        //時間を定義
        elapsedTime += Time.deltaTime;

        //出現させる敵を時間経過に応じて変更
        for (int i = 0; i < enemyChangeIntervals.Length; i++)
        {
            if (elapsedTime >= enemyChangeIntervals[i])
            {
                currentEnemyIndex = i;
            }
        }

        //出現間隔を徐々に短くする
        if (spawnInterval > minSpawnInterval)
        {
            spawnInterval -= spawnIntervalDecreseRate * Time.deltaTime;
        }
        else
        {
            spawnInterval = minSpawnInterval;   //最小値を維持
        }
    }

    //敵生成メソッドの呼び出しとspawnIntervalが来るまで待機の命令
    //コルーチンはシンプルなコードかつUpdate内に書かない為処理も軽くできる
    IEnumerator SpawnEnemies()
    {

        while (true)
        {
            //ゲーム状態がplayingの時だけ敵を生成
            if (a_GameChecker.gameState == "playing")
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        //X座標をランダム化
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 randomPosition = new Vector3(
            randomX,
            spawnPoint.position.y,
            spawnPoint.position.z
            );

        //敵を生成
        Instantiate(enemyPrefabs[currentEnemyIndex], randomPosition, spawnPoint.rotation);
    }

}
