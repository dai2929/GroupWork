using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    bool isReversed = false;    //反転済みかを追跡するフラグ

    // Update is called once per frame
    void Update()
    {
        // gameStateがgameclearの場合は進行方向を逆転させる
        if (a_GameChecker.gameState == "gameclear")
        {
            if (!isReversed)
            {
                //敵を180度回転させる
                transform.Rotate(0, 180, 0);
                isReversed = true;  //反転済みとして記録
            }
            //通常時とは反対方向に進むはず
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            // 通常時はスポーン時の向きに直進
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
