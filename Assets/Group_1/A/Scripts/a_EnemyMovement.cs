using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameStateがplayingじゃなければreturn　一旦ね
        //if (GameChecker.gameState != "playing") return;

        //敵をスポーン時の向きにまっすぐ進ませる
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
