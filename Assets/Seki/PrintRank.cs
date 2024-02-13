using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintRank : MonoBehaviour
{
    [SerializeField, Header("ランク名前")]
    Text[] rankText = new Text[5];

    [SerializeField, Header("表示させるテキスト")]
    Text[] rankingText = new Text[5];

    string[] ranking = { "1位", "2位", "3位", "4位", "5位" };

    string[] king = { "1", "2", "3", "4", "5" };
    string[] rankName = new string[5];
    int[] rankingValue = new int[5];

    [SerializeField] IconController icon;
    [SerializeField] Animator titleIcon;

    private void Start() {
        GetRanking();
    }

    /// <summary>
    /// ランキング呼び出し
    /// </summary>
    void GetRanking() {
        //ランキング呼び出し
        for(int i = 0; i < ranking.Length; i++) {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
            rankName[i] = PlayerPrefs.GetString(king[i]);
            rankingText[i].text = rankingValue[i].ToString();
            rankText[i].text = rankName[i];
        }
    }

    
    public void OnFinishAnim() {
        icon.RANK = false;
        titleIcon.SetBool("title", true);
    }
}
