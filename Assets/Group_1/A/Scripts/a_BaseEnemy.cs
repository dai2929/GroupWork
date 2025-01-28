using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // UIの使用

public class a_BaseEnemy : MonoBehaviour
{
    public int health = 10; // 敵の体力（デフォルト値）
    public GameObject itemPrefab; // ドロップアイテムのプレハブ

    public GameObject hpTextPrefab; // HPテキストのプレハブ
    private TextMeshProUGUI hpText;            // HPテキストの参照
    private GameObject hpTextInstance; // インスタンス化されたテキスト

    void Start()
    {
        a_GameManager.Instance?.RegisterEnemy(this);
        // 既存の処理を保持
        if (hpTextPrefab != null)
        {
            hpTextInstance = Instantiate(hpTextPrefab, Vector3.zero, Quaternion.identity);
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                hpTextInstance.transform.SetParent(canvas.transform, false);
            }
            hpText = hpTextInstance.GetComponent<TextMeshProUGUI>();
            UpdateHPText();
        }
    }

    void Update()
    {
        // HPテキストを敵に追従させる
        if (hpTextInstance != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
            hpTextInstance.transform.position = screenPos;
        }
    }



    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHPText(); // ダメージを受けたらHP表示を更新
        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = health.ToString(); // HPを文字列として更新
        }
    }

    // 仮想メソッドに変更
    public virtual void Die()
    {
        Debug.Log("BaseEnemy has died.");
        Destroy(gameObject); // 基本的な消滅処理
        Destroy(hpTextInstance); // HPテキストを削除
        DropItem();
    }

    public virtual void ReverseDirection()
    {
        // もしオブジェクトが削除されている場合、処理をスキップ
        if (this == null || gameObject == null) return;

        // 敵の速度や方向を反転するロジックを記述
        // 例: Rigidbody の velocity を反転
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = -rb.velocity;
        }
    }

    void DropItem()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
