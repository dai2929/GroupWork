using UnityEngine;

public class a_GreenBot : a_BaseEnemy
{
    // 強力な弾丸のダメージ量を調整
    public int strongBulletDamage = 2;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("StrongBullet"))
        {
            TakeDamage(strongBulletDamage);
            Destroy(other.gameObject);
        }
    }
}
