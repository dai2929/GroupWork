using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_BossBot : a_BaseEnemy
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("StrongBullet"))
        {
            TakeDamage(2);
            Destroy(other.gameObject);
        }
    }

    // 上書きした死亡処理
    public override void Die()
    {
        Debug.Log("ボスを倒した！");
        a_GameChecker.gameState = "gameclear";

        // 他の敵を反転
        a_GameManager.Instance?.ReverseAllEnemies();

        // 基底クラスのDie処理を呼び出す
        base.Die();
    }
}
