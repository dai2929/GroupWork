using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUpgradeItem : MonoBehaviour
{
    public float fireRateBoost = 0.1f;  // 発射間隔短縮量
    public float speedBoost = 2f;       // 弾速増加量

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーのShooterコンポーネントを取得
            a_Shooter shooter = other.GetComponent<a_Shooter>();
            if (shooter != null)
            {
                shooter.UpgradeBullet(fireRateBoost, speedBoost);
            }

            // アイテムを削除
            Destroy(gameObject);
        }
    }
}