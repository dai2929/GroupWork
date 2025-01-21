using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 2;   //敵の最大体力
    int currentHealth;  //変動する体力

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;  //初期化
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    private void OnTriggerEnter(Collider other)
    {
        //Bulletのタグのオブジェクトに当たった場合
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);  //ダメージ処理(ダメージ量は調整可)
            Destroy(other.gameObject);  //弾を消す
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);    //HPが0以下で敵を消滅
        }
    }
}
