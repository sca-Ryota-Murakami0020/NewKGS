using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintRank : MonoBehaviour
{
    //受け取るスコア
    int point;

    //受け取る名前
    string myName;

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

        point = 1000;

        myName = "宮館　先生";

        GetRanking();

        SetRanking(point, myName);

        for(int i = 0; i < rankingText.Length; i++) {
            rankingText[i].text = rankingValue[i].ToString();
            rankText[i].text = rankName[i];
        }
    }

    /// <summary>
    /// ランキング呼び出し
    /// </summary>
    void GetRanking() {
        //ランキング呼び出し
        for(int i = 0; i < ranking.Length; i++) {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
            rankName[i] = PlayerPrefs.GetString(king[i]);
        }
    }

    /// <summary>
    /// ランキング書き込み
    /// </summary>
    void SetRanking(int _value, string namae) {
        //rankingValue[0] = 100;
        //rankName[0] = "moriya";
        //書き込み用
        for(int i = 0; i < ranking.Length; i++) {
            //取得した値とRankingの値を比較して入れ替え
            if(_value > rankingValue[i]) {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
                var name = rankName[i];
                rankName[i] = namae;
                namae = name;
            }
        }

        //入れ替えた値を保存
        for(int i = 0; i < ranking.Length; i++) {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
            PlayerPrefs.SetString(king[i], rankName[i]);
        }
    }
    public void OnFinishAnim() {
        icon.RANK = false;
        titleIcon.SetBool("title", true);
    }
}
