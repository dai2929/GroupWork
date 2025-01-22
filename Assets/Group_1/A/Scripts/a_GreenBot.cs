using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_GreenBot : MonoBehaviour
{
    public GameObject bulletUpgradeItemPrefab; // Bullet強化アイテムのプレハブ
    public int health = 10; // GreenBotの体力


    void OnTriggerEnter(Collider other)
    {
        //Bulletのタグのオブジェクトに当たった場合
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);  //ダメージ処理(ダメージ量は調整可)
            Destroy(other.gameObject);  //弾を消す
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // アイテムを確定でドロップ
        DropItem();
        // 自分自身を削除
        Destroy(gameObject);
    }

    void DropItem()
    {
        if (bulletUpgradeItemPrefab != null)
        {
            Instantiate(bulletUpgradeItemPrefab, transform.position, Quaternion.identity);
        }
    }
}