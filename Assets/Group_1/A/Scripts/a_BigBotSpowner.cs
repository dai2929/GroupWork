using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_BigBotSpowner : MonoBehaviour
{
    public GameObject BigBotPrefab; // でかいロボのプレハブ
    public GameObject BossBotPrefab;    //ボスのプレハブ
    public Transform spawnPoint;    // 出現位置

    public float spawnDelay = 40f;  // ゲーム開始からの出現までの時間（秒）
    public float bossspawn = 60f;   //ボスのスポーン時間

    // Start is called before the first frame update
    void Start()
    {
        // 指定した時間後にスポーンする
        Invoke(nameof(SpawnBigBot), spawnDelay);

        //
        Invoke(nameof(SpawnBossBot), bossspawn);
    }

    void SpawnBigBot()
    {
        if (BigBotPrefab != null && spawnPoint != null)
        {
            // BigBotを生成
            Instantiate(BigBotPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("BigBotが出現しました！");
        }
        else
        {
            Debug.LogWarning("BigBotPrefabまたはspawnPointが設定されていません。");
        }
    }

    void SpawnBossBot()
    {
        if (BossBotPrefab != null && spawnPoint != null)
        {
            // BigBotを生成
            Instantiate(BossBotPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("ボスが出現しました！");
        }
        else
        {
            Debug.LogWarning("BossPrefabまたはspawnPointが設定されていません。");
        }
    }
}
