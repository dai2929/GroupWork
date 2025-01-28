using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_BulletSE : MonoBehaviour
{
    public AudioClip fireSound;  // 発射時の音
    private AudioSource audioSource;

    void Start()
    {
        // AudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        // SEを再生
        PlayFireSound();
    }

    void PlayFireSound()
    {
        if (audioSource != null && fireSound != null)
        {
            audioSource.PlayOneShot(fireSound); // SEを再生
        }
        else
        {
            Debug.LogWarning("AudioSourceまたはAudioClipが設定されていません！");
        }
    }

    // 弾の動きなどの処理はここに追加
}