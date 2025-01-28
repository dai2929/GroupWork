using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_PUpItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーのShooterコンポーネントを取得
            a_Shooter shooter = other.GetComponent<a_Shooter>();
            if (shooter != null)
            {
                shooter.ActivateStrongBulletPermanently(); // 強力弾を有効化
            }

            // アイテムを削除
            Destroy(gameObject);
        }
    }
}
