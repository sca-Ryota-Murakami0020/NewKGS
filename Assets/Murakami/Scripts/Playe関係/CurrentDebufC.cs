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
    private bool flashingMoveIcon = false;
    private float currentJumpDebufTime = 0.0f;
    private int maxJumpDebufTime = 0;
    private bool flashingJumpIcon = false;

    //倍率関係
    private float moveDebufMag = 1.0f;
    private float oldMoveDebufMag = 0.0f;
    private float jumpDebufMag = 1.0f;
    private float oldJumpDebufMag = 0.0f;

    //フラグ関係
    private bool onMoveDebuf = false;
    private bool onJumpDebuf = false;

    //PlayerManager
    private PlayerManager playerManager;

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
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        jumpDebufIcon.enabled = false;
        moveDebufIcon.enabled = false;
        moveDebufMag = playerManager.DefSpeedMag;
        jumpDebufMag = playerManager.DefJumpMag;
    }

    // Update is called once per frame
    void Update()
    {
        if(onMoveDebuf)
            CountMoveDebuf();
        if(onJumpDebuf)
            CountJumpDebuf();
    }

    #region//移動デバフ
    //移動用デバフ起動
    public void ActiveMoveDebuf(int time, float mag)
    {
        //最初（初めてデバフを受けた時）の処理
        if(moveDebufMag == playerManager.DefSpeedMag && !onMoveDebuf)
        {
            onMoveDebuf = true;
            //プレイヤーが他のデバフを受けていない状態なら
            if (!onJumpDebuf)
                playerC.PopDebufEffect();
            moveDebufIcon.enabled = true;
            //差分の計算
            var def = 0.0f;
            if(moveDebufMag > mag) def = moveDebufMag - mag;
            else def = mag - moveDebufMag;

            moveDebufMag = def;
            oldMoveDebufMag = moveDebufMag;

            maxMoveDebufTime = time;
            Debug.Log( "時間：" + maxMoveDebufTime);
        }
        //デバフ中且つ取得した倍率の方が高い場合
        else if (onMoveDebuf)
        { 
            //倍率の比較
            var mag1 = oldMoveDebufMag;
            var mag2 = playerManager.DefSpeedMag - mag;
            //今回の倍率が前回の倍率を上回っている（数値的には小さい方）なら
            if(mag1 > mag2)
            {
                moveDebufMag = mag2;
                //付与される予定の効果時間が前回の付与時間を上回っているなら
                if (maxMoveDebufTime < time)
                {
                    //実行時間のリセット
                    currentMoveDebufTime = 0.0f;
                    maxMoveDebufTime = time;
                }
            }
            //下回っているなら特に何もしない
            else moveDebufMag = mag1;
        }
    }
    //時間計算（移動用）
    private void CountMoveDebuf()
    {
        currentMoveDebufTime += 0.01f;
        Debug.Log("経過時間" + currentMoveDebufTime);
        if(currentMoveDebufTime == maxMoveDebufTime * 0.8 && !flashingMoveIcon)
        {
            StartCoroutine(FlashingMoveDebuf());
            flashingMoveIcon = true;
        }
        if(currentMoveDebufTime >= maxMoveDebufTime)
            EndMoveDebuf();
    }

    //アイコンの点滅
    private IEnumerator FlashingMoveDebuf()
    {
        while (currentMoveDebufTime <= maxMoveDebufTime * 0.99)
        {
            yield return new WaitForSeconds(0.1f);
            moveDebufIcon.enabled = false;
            yield return new WaitForSeconds(0.1f);
            moveDebufIcon.enabled = true;
        }
    }

    //移動デバフの終了
    private void EndMoveDebuf()
    {
        flashingMoveIcon = false;
        onMoveDebuf = false;
        moveDebufIcon.enabled = false;
        moveDebufMag = playerManager.DefSpeedMag;
        currentMoveDebufTime = 0.0f;
        oldMoveDebufMag = 0.0f;
        maxMoveDebufTime = 0;
        if (!onJumpDebuf && !onMoveDebuf)
            playerC.DeleteDebufEffect();
    }
    #endregion

    #region//ジャンプ用
    //ジャンプ用デバフ起動
    public void ActiveJumpDebuf(int time, float mag)
    {
        //最初（初めてデバフを受けた時）の処理
        if (jumpDebufMag == playerManager.DefJumpMag && !onJumpDebuf)
        {
            onMoveDebuf = true;
            //プレイヤーが他のデバフを受けていない状態なら
            if (!onMoveDebuf)
                playerC.PopDebufEffect();
            jumpDebufIcon.enabled = true;
            //差分の計算
            var def = 0.0f;
            if (jumpDebufMag > mag) def = jumpDebufMag - mag;
            else def = mag - jumpDebufMag;

            jumpDebufMag = def;
            oldJumpDebufMag = jumpDebufMag;

            maxMoveDebufTime = time;
        }
        //デバフ中且つ取得した倍率の方が高い場合
        else if (onJumpDebuf)
        {
            //倍率の比較
            var mag1 = oldJumpDebufMag;
            var mag2 = playerManager.DefJumpMag - mag;
            //今回の倍率が前回の倍率を上回っている（数値的には小さい方）なら
            if (mag1 > mag2)
            {
                jumpDebufMag = mag2;
                //付与される予定の効果時間が前回の付与時間を上回っているなら
                if (maxJumpDebufTime < time)
                {
                    //実行時間のリセット
                    currentJumpDebufTime = 0.0f;
                    maxJumpDebufTime = time;
                }
            }
            //下回っているなら特に何もしない
            else jumpDebufMag = mag1;
        }

    }
    //時間計測（ジャンプ用）
    private void CountJumpDebuf()
    {
        currentJumpDebufTime += 0.01f;
        if (currentJumpDebufTime == maxJumpDebufTime * 0.8 && !flashingJumpIcon)
        {
            StartCoroutine(FlashingMoveDebuf());
            flashingJumpIcon = true;
        }
        if (currentJumpDebufTime >= maxJumpDebufTime)
            EndJumpDebuf();
    }

    //点滅処理
    private IEnumerator FlashigJumpIcon()
    {
        while (currentJumpDebufTime <= maxJumpDebufTime)
        {
            yield return new WaitForSeconds(0.1f);
            jumpDebufIcon.enabled = false;
            yield return new WaitForSeconds(0.1f);
            jumpDebufIcon.enabled = true;
        }
    }

    //ジャンプデバフの終了
    private void EndJumpDebuf()
    {
        flashingJumpIcon = false;
        onJumpDebuf = false;
        jumpDebufIcon.enabled = false;
        jumpDebufMag = playerManager.DefJumpMag;
        currentJumpDebufTime = 0.0f;
        oldJumpDebufMag = 0.0f;
        maxJumpDebufTime = 0;
        if(!onJumpDebuf && !onMoveDebuf)
            playerC.DeleteDebufEffect();
    }
    #endregion
}
