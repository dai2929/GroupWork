using System.Collections;
using UnityEngine;

public class a_GreenBotSpawner : MonoBehaviour
{
    public GameObject[] greenBotPrefabs;   // GreenBotのプレハブ(早いタイプ、でか遅いタイプ)
    public Transform spawnPoint;          // 出現位置
    public float spawnRangeX = 3f;        // 出現するX座標範囲
    public float spawnInterval = 10f;     // 出現間隔

    void Start()
    {
        // GreenBotを一定間隔で出現させるコルーチンを開始
        StartCoroutine(SpawnGreenBots());
    }

    IEnumerator SpawnGreenBots()
    {
        // 初回の出現間隔を待つ
        yield return new WaitForSeconds(spawnInterval);

        while (true)
        {
            if (a_GameChecker.gameState == "playing")
            {
                SpawnGreenBot();
            }

            // 出現間隔を待つ
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnGreenBot()
    {
        // 出現位置のX座標をランダム化
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(
            randomX,
            spawnPoint.position.y,
            spawnPoint.position.z
        );

        // GreenBotの種類をランダムに選択
        if (greenBotPrefabs.Length > 0)
        {
            int botIndex = Random.Range(0, greenBotPrefabs.Length);
            GameObject selectedBotPrefab = greenBotPrefabs[botIndex];

            // GreenBotを生成
            Instantiate(selectedBotPrefab, spawnPosition, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("No GreenBot prefabs assigned!");
        }
    }
}
