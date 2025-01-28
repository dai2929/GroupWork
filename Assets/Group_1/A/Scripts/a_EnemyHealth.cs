using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class a_EnemyHealth : MonoBehaviour
{
    public int maxHealth = 2;    // 敵の最大体力
    private int currentHealth;  // 現在の体力

    public GameObject hpTextPrefab;  // HP表示用のテキストプレハブ
    private TextMeshProUGUI hpText;             // HP表示用のテキスト
    private GameObject hpTextInstance; // 生成されたテキストインスタンス

    public Transform hpTextPosition; // HPテキストの表示位置（Transform参照）

    void Start()
    {
        currentHealth = maxHealth; // 初期化

        // HPテキストの生成と設定
        if (hpTextPrefab != null)
        {
            hpTextInstance = Instantiate(hpTextPrefab, Vector3.zero, Quaternion.identity);

            // Canvas2を探して親に設定
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                hpTextInstance.transform.SetParent(canvas.transform, false);
            }
            else
            {
                Debug.LogError("Canvas が見つかりませんでした");
            }

            // HPテキストを取得
            hpText = hpTextInstance.GetComponentInChildren<TextMeshProUGUI>();
            if (hpText != null)
            {
                UpdateHPText();
            }
            else
            {
                Debug.LogWarning("HPテキストが設定されていません");
            }
        }
        else
        {
            Debug.LogError("HPテキストプレハブがアタッチされていません");
        }
    }

    void Update()
    {
        if (hpTextInstance != null)
        {
            // HPテキストの位置を敵に追従させる
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(hpTextPosition.position);
            hpTextInstance.transform.position = screenPosition;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 通常弾丸に当たった場合
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);  // 通常弾のダメージ処理
            Destroy(other.gameObject);       // 弾を消す
        }

        // 強力な弾丸に当たった場合
        if (other.CompareTag("StrongBullet"))
        {
            TakeDamage(2);  // 強力弾のダメージ処理
            Destroy(other.gameObject);       // 弾を消す
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        UpdateHPText();

        if (currentHealth <= 0)
        {
            Destroy(hpTextInstance); // HPテキストを削除
            Destroy(gameObject);    // 敵を削除
        }
    }

    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = $"{currentHealth}"; // テキストを更新
        }
    }

    private void OnDestroy()
    {
        if (hpTextInstance != null)
        {
            Destroy(hpTextInstance); // 敵が破壊されたらHPテキストも削除
        }
    }
}
