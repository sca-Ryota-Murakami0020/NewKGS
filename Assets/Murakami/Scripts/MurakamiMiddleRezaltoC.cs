using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MurakamiMiddleRezaltoC : MonoBehaviour
{
    [SerializeField]
    private Animator ani;
    [SerializeField]
    private GameManager gameManager;

    [Header("数字"),SerializeField]
    private Sprite[] numberImage;
    [Header("タイム表記Image"),SerializeField]
    private Image[] timeImage;
    [Header("タイムボーナス表記Image"),SerializeField]
    private Image[] timeBounusImage;

    [Header("残機表記Image"),SerializeField]
    private Image[] remainImage;
    [Header("残機ボーナス表記Image"),SerializeField]
    private Image[] remainBounusImage;
    
    [Header("難易度表記Image"),SerializeField]
    private Image[] difImage;
    [Header("難易度ボーナス表記Image"),SerializeField]
    private Image[] difBounusImage;

    [Header("トータルスコア表示Image"),SerializeField]
    private Image[] totalScoreImage;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        for(int i = 0; i < timeImage.Length; i++) ResetImage(timeImage[i]);

        for(int i = 0; i < timeBounusImage.Length; i++) ResetImage(timeBounusImage[i]);

        for(int i = 0; i < remainImage.Length; i++) ResetImage(remainImage[i]);

        for(int i = 0; i < remainBounusImage.Length; i++) ResetImage(remainBounusImage[i]);

        for(int i = 0; i < difImage.Length; i++) ResetImage(difImage[i]);

        for(int i = 0; i < difBounusImage.Length; i++) ResetImage(difBounusImage[i]);

        for(int i = 0; i < totalScoreImage.Length; i++) ResetImage(totalScoreImage[i]);
    }

    //Imageの初期化関数
    public void ResetImage(Image image)
    {
        image.sprite = numberImage[0];
        image.enabled = false;
    }

    public void StartMoveBack()
    {
        ani.SetTrigger("ShowRezalto");
    }

    //リザルト用
    public void StartShowRezalto()
    {
        //計算開始
        StartCoroutine(CountRezalt());
    }


    //中間リザルト
    private IEnumerator CountRezalt()
    {
        //時間表記用変数
        int time = gameManager.StageCount;
        //時間ボーナス
        int timeScore = gameManager.StageCount * 10;
        //残機
        int remain = gameManager.CurrentRemain;
        //残機ボーナス
        int remainScore = gameManager.CurrentRemain * 1000;
        //難易度
        int dif = gameManager.DifLevel;
        //難易度ボーナス
        int difScore = dif * 1000;
        //ボーナス加算後のトータルスコア
        int stageTotalScore = gameManager.GetScore + timeScore + remainScore + difScore;

        //タイム表記
        int count = 0;
        int localScore = time;
        while (count <= time)
        {
            for(int i = 0; i < timeImage.Length; i++)
            {
                int num = localScore % 10;
                //累乗して表示するImageを選択する。
                if(count >= Mathf.Pow(10, i)) timeImage[i].enabled = true;
                timeImage[i].sprite = numberImage[num];
                localScore /= 10;
                yield return new WaitForSeconds(0.001f);
            }
            count++;
        }
        yield return new WaitForSeconds(1.2f);

        //タイムボーナス表記
        count = 0;
        localScore = timeScore;
        while(count <= timeScore)
        {
            for (int i = 0; i < timeBounusImage.Length; i++)
            {
                int num = localScore % 10;
                //累乗して表示するImageを選択する。
                if (count >= Mathf.Pow(10, i)) timeBounusImage[i].enabled = true;
                timeBounusImage[i].sprite = numberImage[num];
                localScore /= 10;
                yield return new WaitForSeconds(0.001f);
            }
            count++;
        }
        yield return new WaitForSeconds(1.2f);

        //残機表記
        count = 0;
        localScore = remainScore;
        while (count <= remainScore)
        {
            for (int i = 0; i < remainImage.Length; i++)
            {
                int num = localScore % 10;
                //累乗して表示するImageを選択する。
                if (count >= Mathf.Pow(10, i)) remainImage[i].enabled = true;
                remainImage[i].sprite = numberImage[num];
                localScore /= 10;
                yield return new WaitForSeconds(0.001f);
                count++;
            }
        }
        yield return new WaitForSeconds(1.2f);

        //残機ボーナス表記
        count = 0;
        localScore = remainScore;
        while (count <= remainScore)
        {
            for (int i = 0; i < remainBounusImage.Length; i++)
            {
                int num = localScore % 10;
                //累乗して表示するImageを選択する。
                if (count >= Mathf.Pow(10, i)) remainBounusImage[i].enabled = true;
                remainBounusImage[i].sprite = numberImage[num];
                localScore /= 10;
                yield return new WaitForSeconds(0.001f);
            }
            count++;
        }
        yield return new WaitForSeconds(1.2f);

        //難易度
        count = 0;
        localScore = dif;
        while (count <= dif)
        {
            for (int i = 0; i < difImage.Length; i++)
            {
                int num = localScore % 10;
                //累乗して表示するImageを選択する。
                if (count >= Mathf.Pow(10, i)) difImage[i].enabled = true;
                difImage[i].sprite = numberImage[num];
                localScore /= 10;
                yield return new WaitForSeconds(0.001f);
            }
            count++;
        }
        yield return new WaitForSeconds(1.2f);

        //難易度ボーナス
        count = 0;
        localScore = difScore;
        while (count <= difScore)
        {
            for (int i = 0; i < difBounusImage.Length; i++)
            {
                int num = localScore % 10;
                //累乗して表示するImageを選択する。
                if (count >= Mathf.Pow(10, i)) difBounusImage[i].enabled = true;
                difBounusImage[i].sprite = numberImage[num];
                localScore /= 10;
                yield return new WaitForSeconds(0.001f);
            }
            count++;
        }
        yield return new WaitForSeconds(1.2f);

        //トータルスコア
        count = 0;
        localScore = stageTotalScore;
        while (count <= stageTotalScore)
        {
            for (int i = 0; i < totalScoreImage.Length; i++)
            {
                int num = localScore % 10;
                //累乗して表示するImageを選択する。
                if (count >= Mathf.Pow(10, i)) totalScoreImage[i].enabled = true;
                totalScoreImage[i].sprite = numberImage[num];
                localScore /= 10;
                yield return new WaitForSeconds(0.001f);
            }
            count++;
        }
        yield return new WaitForSeconds(1.2f);
    }
}
