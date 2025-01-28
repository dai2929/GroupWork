using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_BulletsDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
        }

        //強化弾も消す
        if (other.gameObject.tag == "StrongBullet")
        {
            Destroy(other.gameObject);
        }
    }
}
