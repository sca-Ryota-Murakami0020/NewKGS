using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RezalutRank : MonoBehaviour
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
    PlayerManager playerManager;

    bool rank = false;

    

    bool go = false;
    public bool GO {
        set {
            this.go = value;
        }
        get {
            return this.go;
        }
    }

    [SerializeField] GameObject zannen;
    [SerializeField] NameScripts userName;
    [SerializeField] LetterPanel letter;

    [SerializeField] GameObject inputName;
    [SerializeField] GameObject rankIn;

    [SerializeField] RezalutC rezalutC;

    void Start()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        point = playerManager.GameLevel;
        GetRanking();
        Ranking(point);
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
            if(_value >= rankingValue[i]) {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
                var name = rankName[i];
                rankName[i] = namae;
                namae = name;
            }
            rankingText[i].text = rankingValue[i].ToString();
            rankText[i].text = rankName[i];
        }

        //入れ替えた値を保存
        for(int i = 0; i < ranking.Length; i++) {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
            PlayerPrefs.SetString(king[i], rankName[i]);
        }
        go = true;
    }

    void Ranking(int _value) {
        for(int i = 0; i < ranking.Length; i++) {
            //取得した値とRankingの値を比較して入れ替え
            if(_value >= rankingValue[i]) {
                rank = true;
            } else {
                rank = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rank);
        if(rank && rezalutC.PUSH) {
            StartCoroutine(waitActive(rank, rezalutC.PUSH));
        } else if(!rank && rezalutC.PUSH){
            StartCoroutine(waitActive(rank, rezalutC.PUSH));
        }

        if(letter.NEXT)
        {
            rankIn.SetActive(true);
            name = userName.MYNAME;
            SetRanking(point, name);
        }
    }

    IEnumerator waitActive(bool rank,bool push) {
        yield return new WaitForSeconds(1.0f);
        if(rank && push) {
            inputName.SetActive(true);
        } else if(!rank && push) {
            zannen.SetActive(true);
        }
    }
}
