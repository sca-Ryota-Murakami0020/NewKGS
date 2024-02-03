using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvoidanceC : MonoBehaviour
{
    [Header("回避ゲージ（中身、中身（赤）、外の順で）"),SerializeField]
    private Image[] avoiGauge;
    [Header("回避のクールタイム"),SerializeField]
    private int avoiCoolTime;
    [Header("ゲージの表示時間"),SerializeField]
    private int showAvoiGaugeTime;

    public enum AvoiPlam
    {
        CanUse,
        Doing,
        CoolTime
    };

    private AvoiPlam avoiPlam = AvoiPlam.CanUse;

    //実行中のゲージ時間
    private float currentAvoiGauge = 0.0f;
    //実行中のゲージ表示時間
    private float currentShowGaugeTime = 0.0f;
    //表示時間計算
    private bool countShowTime = false;

    public AvoiPlam AvoiP
    {
        get { return this.avoiPlam;}
        set { this.avoiPlam = value;}
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int count = 0; count < avoiGauge.Length; count++)
            avoiGauge[count].enabled = false;
        currentAvoiGauge = avoiCoolTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(avoiPlam == AvoiPlam.CoolTime)
            RecoverAvoidanceGauge();
        if(countShowTime)
            CountShowGauge();
    }

    //ゲージ使用
    public void UsingAvoidanceGauge()
    {
        //赤のゲージ部分と黒のゲージのみ表示する。
        for(int count = 1; count < avoiGauge.Length; count++)
            avoiGauge[count].enabled = true;
        currentAvoiGauge = 0.0f;
        avoiGauge[1].fillAmount = 0.0f;
    }

    //ゲージ回復
    private void RecoverAvoidanceGauge()
    {
        currentAvoiGauge += 0.05f;
        var rate = currentAvoiGauge / avoiCoolTime;
        avoiGauge[1].fillAmount = rate;
        if(rate >= 1)
        {
            currentAvoiGauge = avoiCoolTime;
            avoiGauge[0].enabled = true;
            countShowTime = true;
            avoiPlam = AvoiPlam.CanUse;
        }
    }

    //ゲージ表示時間計測
    private void CountShowGauge()
    {
        currentShowGaugeTime += 0.07f;
        if(currentShowGaugeTime >= showAvoiGaugeTime)
        {
            for(int count = 0; count < avoiGauge.Length; count++)
                avoiGauge[count].enabled = false;
            currentShowGaugeTime = 0.0f;
            countShowTime = false;
        }
    }
}
