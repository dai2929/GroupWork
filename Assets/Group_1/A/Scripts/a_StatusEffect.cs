using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class a_StatusEffect : MonoBehaviour
{
    public TextMeshProUGUI[] statusTexts;  //対象のステータステキスト

    public Color[] originalColors;    //元のテキストカラー
    public Color highlightColor = Color.red;    //強調表示する色
    public float highlightDuration = 1.5f;  //強調表示する時間

    public float blinkInterval = 0.2f; // 点滅の間隔

    // Start is called before the first frame update
    void Start()
    {
        // 元のテキストカラーを保存
        originalColors = new Color[statusTexts.Length];
        for (int i = 0; i < statusTexts.Length; i++)
        {
            if (statusTexts[i] != null)
            {
                originalColors[i] = statusTexts[i].color;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ステータスアップ時にこのメソッドを呼び出す
    public void HighlightStatus(int index,string statusChangeText)
    {
        if (index >= 0 && index < statusTexts.Length && statusTexts[index] != null)
        {
            // 指定したインデックスのテキストを更新
            statusTexts[index].text = statusChangeText;

            // 点滅アニメーション開始
            StartCoroutine(BlinkCoroutine(index));
        }
    }

    IEnumerator BlinkCoroutine(int index)
    {
        float elapsedTime = 0f;
        bool isHighlighted = true;

        while (elapsedTime < highlightDuration)
        {
            // 点滅：強調色と元の色を交互に切り替え
            statusTexts[index].color = isHighlighted ? highlightColor : originalColors[index];
            isHighlighted = !isHighlighted;

            // 点滅間隔分待機
            yield return new WaitForSeconds(blinkInterval);

            elapsedTime += blinkInterval;
        }

        // 最後に元の色とテキストに戻す
        statusTexts[index].color = originalColors[index];
    }
}
