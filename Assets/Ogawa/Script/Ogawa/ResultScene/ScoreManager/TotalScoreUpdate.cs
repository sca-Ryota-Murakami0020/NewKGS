using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScoreUpdate : MonoBehaviour
{
    //数字の画像
    [SerializeField] private Sprite[] numberImage;
    //数字の配置位置
    [SerializeField] private Image[] imageNumber;
    //カンマの配置
    //[SerializeField] private Image[] commaImage;

    //更新前のスコア
    private int oldScore = 0;
    //現在のスコア
    private int _totalScore = 15000;

    private bool delayComplete = false;

    private float _currentDisplayScore = 0f;

    // Start is called before the first frame update
    void Start()
    {

        //数値の初期化
        for (int count = 0; count < imageNumber.Length; count++)
        {
            imageNumber[count].sprite = numberImage[0];
            //スコアを初期表示(0,000)にする
            foreach (Image imageNum in imageNumber)
            {
                imageNum.enabled = false;
            }
            //if (count < 3) imageNumber[count].enabled = true;
            //if (count >= 4) imageNumber[count].enabled = false;
        }

        //コンマ表示の初期化
        //commaImage[1].enabled = false;
        StartCoroutine(DelayedStart());

    }

    private void Update()
    {
        bool isGameClear = SlideUIControl.Instance.GameClear;

        if (isGameClear)
        {
            if (delayComplete)
            {
                UpdateScore();
                ShowScore((int)_currentDisplayScore);
            }
        }

    }
    //テスト用
    private void FixedUpdate()
    {
        //Debug.Log("スコア：" + _score);
        //Debug.Log("OldScore：" + oldScore);
    }

    //表示スコアの更新
    public void UpdateScore()
    {
        if (_currentDisplayScore == _totalScore) return;
        float localOldScore = _currentDisplayScore;
        if (_totalScore - _currentDisplayScore < 0) _currentDisplayScore += (float)_totalScore - _currentDisplayScore;
        else
        {
            //0.3秒でスコア加算の演出を終わらせるための変数  
            float _scoreMaxSec = 0.333f;
            //１ずつ加算していくようにするための加算方法
            localOldScore += (_totalScore - oldScore) / (_scoreMaxSec / Time.deltaTime);
            _currentDisplayScore = (int)localOldScore;
        }
        localOldScore = _currentDisplayScore;

        for (int count = 0; count < imageNumber.Length; count++)
        {
            int showNum = (int)localOldScore % 10;
            //Debug.Log(showNum);

            imageNumber[count].sprite = numberImage[(int)showNum];
            localOldScore /= 10;
        }
    }

    //表示するスコアの桁数・コンマの表示を更新
    public void ShowScore(int score)
    {
        int calcScore = score;
        int nums = 0;

        while (calcScore > 0)
        {
            imageNumber[nums].enabled = true;
            calcScore /= 10;
            if (nums >= 6)
            {
                Debug.Log(score);
                //commaImage[1].enabled = true;
            }
            nums++;
        }
        if (score > 1000) return;
        for (int count = 0; count < 4; count++)
        {

            imageNumber[count].enabled = true;
        }
        ///commaImage[0].enabled = true;
    }
    IEnumerator DelayedStart()
    {
        // 3秒待機する
        yield return new WaitForSeconds(6.0f);

        // 開始を遅らせる処理
        delayComplete = true;
    }
}