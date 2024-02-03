using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentDebufC : MonoBehaviour
{
    [Header("デバフ中に呼び出すエフェクト"),SerializeField]
    private ParticleSystem debufEffect;
    [Header("ジャンプデバフアイコン"),SerializeField]
    private Image jumpDebufIcon;
    [Header("移動デバフアイコン"),SerializeField]
    private Image moveDebufIcon;
    [SerializeField] private PlayerC playerC;

    //時間関係
    private float currentMoveDebufTime = 0.0f;
    private int maxMoveDebufTime = 0;
    private float currentJumpDebufTime = 0.0f;
    private int maxJumpDebufTime = 0;

    //倍率関係
    private float moveDebufMag = 1.0f;
    private float oldMoveDebufMag = 0.0f;
    private float jumpDebufMag = 1.0f;
    private float oldJumpDebufMag = 0.0f;

    //フラグ関係
    private bool onMoveDebuf = false;
    private bool onJumpDebuf = false;

    public float MoveDebufMag
    {
        get { return this.moveDebufMag; }
    }
    public float JumpDebufMag
    {
        get { return this.jumpDebufMag;}
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpDebufIcon.enabled = false;
        moveDebufIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(onMoveDebuf)
            CountMoveDebuf();
        if(onJumpDebuf)
            CountJumpDebuf();
    }

    //移動用デバフ起動
    public void ActiveMoveDebuf(int time, float mag)
    {
        if (onMoveDebuf && oldMoveDebufMag < mag) moveDebufMag = 1.0f;
        else
        {
            onMoveDebuf = true;
            //プレイヤーが他のデバフを受けていない状態なら
            if (!onJumpDebuf)
            {
                ParticleSystem newPar = Instantiate(debufEffect);
                newPar.transform.position = playerC.PopObject.transform.position;
                newPar.Play();
            }
        }

        moveDebufIcon.enabled = true;
        moveDebufMag -= mag;
        oldMoveDebufMag = mag;
        maxMoveDebufTime = time;
    }
    //時間計算（移動用）
    private void CountMoveDebuf()
    {
        currentMoveDebufTime += 0.01f;
        if(currentMoveDebufTime >= maxMoveDebufTime)
            EndMoveDebuf();
    }
    //移動デバフの終了
    private void EndMoveDebuf()
    {
        onMoveDebuf = false;
        moveDebufIcon.enabled = false;
        moveDebufMag = 1.0f;
        currentMoveDebufTime = 0.0f;
        oldMoveDebufMag = 0.0f;
        maxMoveDebufTime = 0;
        if (!onJumpDebuf && !onMoveDebuf)
            Destroy(this.debufEffect);
    }

    //ジャンプ用デバフ起動
    public void ActiveJumpDebuf(int time, float mag)
    {
        if(onJumpDebuf && oldJumpDebufMag < mag) jumpDebufMag = 1.0f;
        else
        {
            onJumpDebuf = true;
            //プレイヤーが他のデバフを受けていない状態なら
            if (!onMoveDebuf)
            {
                ParticleSystem newPar = Instantiate(debufEffect);
                newPar.transform.position = playerC.PopObject.transform.position;
                newPar.Play();
            }
        }

        jumpDebufIcon.enabled = true;
        jumpDebufMag = jumpDebufMag - mag;
        oldJumpDebufMag = mag;
        maxJumpDebufTime = time;

    }
    //時間計測（ジャンプ用）
    private void CountJumpDebuf()
    {
        currentJumpDebufTime += 0.01f;
        if(currentJumpDebufTime >= maxJumpDebufTime)
            EndJumpDebuf();
    }
    //ジャンプデバフの終了
    private void EndJumpDebuf()
    {
        onJumpDebuf = false;
        jumpDebufIcon.enabled = false;
        jumpDebufMag = 1.0f;
        currentJumpDebufTime = 0.0f;
        oldJumpDebufMag = 0.0f;
        maxJumpDebufTime = 0;
        if(!onJumpDebuf && !onMoveDebuf)
            Destroy(this.debufEffect);
    }

}
