using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_ItemMover : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    private Transform player;    // プレイヤーのTransform

    void Start()
    {
        // プレイヤーのTransformを取得
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object with 'Player' tag not found!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // プレイヤーの方向を計算
            Vector3 direction = (player.position - transform.position).normalized;

            // プレイヤーの方向に向かって移動
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}