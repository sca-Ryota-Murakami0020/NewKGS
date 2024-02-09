using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintRank : MonoBehaviour
{
    //�󂯎��X�R�A
    int point;

    //�󂯎�閼�O
    string myName;

    [SerializeField, Header("�����N���O")]
    Text[] rankText = new Text[5];

    [SerializeField, Header("�\��������e�L�X�g")]
    Text[] rankingText = new Text[5];

    string[] ranking = { "1��", "2��", "3��", "4��", "5��" };

    string[] king = { "1", "2", "3", "4", "5" };
    string[] rankName = new string[5];
    int[] rankingValue = new int[5];

    [SerializeField] IconController icon;
    [SerializeField] Animator titleIcon;

    private void Start() {

        point = 1000;

        myName = "�{�ف@�搶";

        GetRanking();

        SetRanking(point, myName);

        for(int i = 0; i < rankingText.Length; i++) {
            rankingText[i].text = rankingValue[i].ToString();
            rankText[i].text = rankName[i];
        }
    }

    /// <summary>
    /// �����L���O�Ăяo��
    /// </summary>
    void GetRanking() {
        //�����L���O�Ăяo��
        for(int i = 0; i < ranking.Length; i++) {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
            rankName[i] = PlayerPrefs.GetString(king[i]);
        }
    }

    /// <summary>
    /// �����L���O��������
    /// </summary>
    void SetRanking(int _value, string namae) {
        //rankingValue[0] = 100;
        //rankName[0] = "moriya";
        //�������ݗp
        for(int i = 0; i < ranking.Length; i++) {
            //�擾�����l��Ranking�̒l���r���ē���ւ�
            if(_value > rankingValue[i]) {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
                var name = rankName[i];
                rankName[i] = namae;
                namae = name;
            }
        }

        //����ւ����l��ۑ�
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
