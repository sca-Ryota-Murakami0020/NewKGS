using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class NumberImageChange : MonoBehaviour
{
    [System.Serializable]
    public class ScoreRow
    {
        public Image[] scoreDigits;
        public string[] playerName;
    }



    [SerializeField] Sprite[] numberSprites;// 0から9までの数字画像を保持する配列
    [SerializeField] ScoreRow[] ScoreRows;  // スコアの各桁の画像を表示するImageコンポーネントの配列
    [SerializeField] Text[] nameText;       //PlayerNameのtextコンポーネントの配列
    int count = 0;
    // ランキングデータのスコアをUIで表示する
    public void ShowScoreImages(int score, int c)
    {

        string scoreString = score.ToString();// スコアを文字列に変換
        for (int j = 0; j < 7; j++)
        {
            if (j < scoreString.Length)
            {
                int digit = score % 10; // 各桁の数字を取得
                score = score / 10;
                ScoreRows[c].scoreDigits[j].sprite = numberSprites[digit]; // 対応する画像を設定
                ScoreRows[c].scoreDigits[j].gameObject.SetActive(true); // 画像を表示
            }
            else
            {
                ScoreRows[c].scoreDigits[j].gameObject.SetActive(false); // 桁が足りない場合は非表示にする
            }
        }

    }
    //ランキングのplayernameの表示
    public void ShowName(string name, int i)
    {
        nameText[i].text = name;

    }
}
