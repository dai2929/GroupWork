using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_GunController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveLimit = 5f;    //左右の移動制限

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //playingの時以外は動作しない
        if (a_GameChecker.gameState != "playing") return;

        //左右の入力を受け取る
        float horizontalInput = Input.GetAxis("Horizontal");    //A/Dキーまたは左右矢印キー
        Vector3 moveDirection = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection);

        //移動制限を適用
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -moveLimit, moveLimit);
        transform.position = currentPosition;
    }

}
