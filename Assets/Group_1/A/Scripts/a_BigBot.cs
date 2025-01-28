using UnityEngine;

public class a_BigBot : a_BaseEnemy
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
}
