using System.Collections;
using UnityEngine;

public class a_Shooter : MonoBehaviour
{
    public GameObject normalBulletPrefab;   // 通常弾のプレハブ
    public GameObject strongBulletPrefab;  // 強力な弾のプレハブ
    public float fireRate = 0.5f;          // 弾を撃つ間隔
    public float bulletSpeed = 10f;        // 弾の速度

    public Transform bulletParentTransform; // HierarchyでBulletsグループがどれか指定
    public Transform shootPoint;            // 発射位置(銃口)

    public float currentFireRate;          // 現在の発射間隔
    public float currentBulletSpeed;       // 現在の弾の速度
    private GameObject currentBulletPrefab; // 現在の弾丸のプレハブ

    public GameObject CurrentBulletPrefab
    {
        get { return currentBulletPrefab; }
    }

    void Start()
    {
        // 初期値を設定
        currentFireRate = fireRate;
        currentBulletSpeed = bulletSpeed;
        currentBulletPrefab = normalBulletPrefab; // 最初は通常弾を設定

        // 一定間隔で弾を発射
        InvokeRepeating(nameof(FireBullet), 0f, currentFireRate);
    }

    // 弾を発射するメソッド
    void FireBullet()
    {
        // gameStateがplayingでなければ動かない
        if (a_GameChecker.gameState != "playing") return;

        // shootPointがnullでない場合に弾を発射
        if (shootPoint) FireFromPoint(shootPoint);
        else Debug.LogWarning("銃口無し！");
    }

    // 指定した位置から弾を発射
    void FireFromPoint(Transform shootPoint)
    {
        // 弾の生成
        GameObject bullet = Instantiate(currentBulletPrefab, shootPoint.position, Quaternion.identity);

        // 生成したbulletの親にBulletsを指定
        bullet.transform.parent = bulletParentTransform;

        // 発射
        if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
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

        Debug.Log($"発射間隔: {currentFireRate}, 弾速: {currentBulletSpeed}");

        // 弾速アップ時
        GameObject.Find("StatusManager").GetComponent<a_StatusEffect>().HighlightStatus(1, "弾速UP");

        //連射速度が0.1fならもう最大なので強調表示しない
        if (currentFireRate == 0.1f) return;

        // 連射速度アップ時
        GameObject.Find("StatusManager").GetComponent<a_StatusEffect>().HighlightStatus(0, "連射速度UP");
    }

    // 強力な弾丸を永続的に使用する
    public void ActivateStrongBulletPermanently()
    {
        currentBulletPrefab = strongBulletPrefab; // 強力な弾丸に切り替え

        // 威力アップ時
        GameObject.Find("StatusManager").GetComponent<a_StatusEffect>().HighlightStatus(2, "威力UP");
    }
}
