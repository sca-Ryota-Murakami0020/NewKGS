using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThirdStageGM : MonoBehaviour
{
    [SerializeField]
    private RunOnlyPlayerC runPlayerC;
    //[SerializeField]
    //private TMP_Text remainText;
    //実行中の残機
    private int currentRemain = 0;
    //残機用のアニメーション
    [SerializeField]
    private Animator remainAni;
    //ステージ間で残機を管理しているもの
    private PlayerManager playerManager = null;

    //プレイヤーのスピード関係
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float playerJumpPow;
    //デフォのスピード
    private bool debufMove = false;
    private float defMove = 0.0f;
    private bool debufJump = false;
    private float defJump = 0.0f;

    //更新用
    private float oldSpeedMag = 0.0f;
    private float oldJumpMag = 0.0f;

    //時間計算用
    private float moveTime = 0.0f;
    private float jumpTime = 0.0f;

    public float PlayerSpeed
    {
        get { return this.playerSpeed;}
    }

    public float PlayerJumpPow
    {
        get { return this.playerJumpPow;}
    }

    //[SerializeField] GameObject gameClear;
    //[SerializeField] GameObject gameOver;

    

    // Start is called before the first frame update
    void Start()
    {
       
        //初期値の記憶
        defMove = playerSpeed;
        defJump = playerJumpPow;

        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        playerSpeed = playerSpeed * playerManager.DefSpeedMag;
        playerJumpPow = playerJumpPow * playerManager.DefJumpMag;

        currentRemain = playerManager.ManagerRemain;
    }

    void Updata()
    {
        //if(debufMove) CountMoveDebuf();
        //if(debufJump) CountJumpDebuf();
    }

    //開始（動き）
    public void DebufMoveSpeed(float mag)
    {
        if(oldSpeedMag < mag) oldSpeedMag = mag;
        else mag = oldSpeedMag;

        playerSpeed = defMove;
        //再定義
        playerSpeed = playerSpeed * (playerManager.DefSpeedMag - mag);
        if(!debufMove) debufMove = true;
        else moveTime = 0.0f;
    }

    public void DebufJumpSpeed(float mag)
    {
        if(oldJumpMag < mag) oldJumpMag = mag;
        else mag = oldJumpMag;

         playerJumpPow = defJump;
        //再定義
        playerJumpPow = playerJumpPow * (playerManager.DefJumpMag - mag);
        if(!debufJump) debufJump = true;
        else jumpTime = 0.0f;
    }

    //時間計算
    private void CountMoveDebuf()
    {
        moveTime += 0.01f;
        if(moveTime >= 10.0f)
        {
            return;
        }
    }

    private void CountJumpDebuf()
    {
        jumpTime += 0.01f;
        if(jumpTime >= 10.0f)
        {
            jumpTime = 0.0f;
            return;
        }
    }

    //ステージ予備
    public void StageClear()
    {
        SousaUIContorller.stageClear++;
        if(StageSelectController.mode == StageSelectController.MODE.STORY) {
            TitleManager.sceneName = "MojiHyouji";
            //SceneManager.LoadScene("LoadScene");
        }
        else if(StageSelectController.mode == StageSelectController.MODE.CHALLENGE) {
            TitleManager.sceneName = "Masaki";
        }
        
        
    }
}
