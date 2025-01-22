using System.Collections;
using UnityEngine;

public class a_Shooter : MonoBehaviour
{
    public GameObject BulletPrefab;      // 弾のプレハブ
    public float fireRate = 0.5f;        // 弾を撃つ間隔
    public float bulletSpeed = 10f;      // 弾の速度
    public Transform bulletParentTransform; // HierarchyでBulletsグループがどれか指定

    float currentFireRate;       // 現在の発射間隔
    float currentBulletSpeed;    // 現在の弾の速度

    void Start()
    {
        // 初期値を設定
        currentFireRate = fireRate;
        currentBulletSpeed = bulletSpeed;

        // 一定間隔で弾を発射
        InvokeRepeating(nameof(FireBullet), 0f, currentFireRate);
    }

    void FireBullet()
    {
        // gameStateがplayingでなければ動かない
        if (a_GameChecker.gameState != "playing") return;

        // 弾の生成
        GameObject bullet = Instantiate(
            BulletPrefab,
            transform.position,
            Quaternion.identity
        );

        // 生成したbulletの親にBulletsを指定
        bullet.transform.parent = bulletParentTransform;

        // 発射
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * currentBulletSpeed;
        }
    }

    // 弾の強化機能
    public void UpgradeBullet(float fireRateBoost, float speedBoost)
    {
        // InvokeRepeatingを再設定
        CancelInvoke(nameof(FireBullet));

        currentFireRate = Mathf.Max(0.1f, currentFireRate - fireRateBoost); // 発射間隔の最小値を0.1秒に制限
        currentBulletSpeed += speedBoost;

        // 再度InvokeRepeatingで更新
        InvokeRepeating(nameof(FireBullet), 0f, currentFireRate);

        Debug.Log($"Bullet upgraded! New Fire Rate: {currentFireRate}, New Bullet Speed: {currentBulletSpeed}");
    }
}
