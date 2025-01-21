using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject BulletPrefab;    //弾のプレハブ(増築予定)
    public float fireRate = 0.5f;   //弾を撃つ間隔
    public float bulletSpeed = 10f; //弾の速度

    public Transform bulletParentTransform; //HierarchyでBulletsグループがどれか指定

    // Start is called before the first frame update
    void Start()
    {
        //一定間隔で弾を発射
        InvokeRepeating(nameof(FireBullet), 0f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ある条件で弾を変更


    void FireBullet()
    {
        //弾の生成
        GameObject bullet = Instantiate(
            BulletPrefab,
            transform.position,
            Quaternion.identity
            );

        //生成したbulletの親にBulletsを指名
        bullet.transform.parent = bulletParentTransform;

        //発射
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }
}
