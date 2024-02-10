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
    //���s���̎c�@
    private int currentRemain = 0;
    //�c�@�p�̃A�j���[�V����
    [SerializeField]
    private Animator remainAni;
    //�X�e�[�W�ԂŎc�@���Ǘ����Ă������
    private PlayerManager playerManager = null;

    //�v���C���[�̃X�s�[�h�֌W
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float playerJumpPow;
    //�f�t�H�̃X�s�[�h
    private bool debufMove = false;
    private float defMove = 0.0f;
    private bool debufJump = false;
    private float defJump = 0.0f;

    //�X�V�p
    private float oldSpeedMag = 0.0f;
    private float oldJumpMag = 0.0f;

    //���Ԍv�Z�p
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
       
        //�����l�̋L��
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

    //�J�n�i�����j
    public void DebufMoveSpeed(float mag)
    {
        if(oldSpeedMag < mag) oldSpeedMag = mag;
        else mag = oldSpeedMag;

        playerSpeed = defMove;
        //�Ē�`
        playerSpeed = playerSpeed * (playerManager.DefSpeedMag - mag);
        if(!debufMove) debufMove = true;
        else moveTime = 0.0f;
    }

    public void DebufJumpSpeed(float mag)
    {
        if(oldJumpMag < mag) oldJumpMag = mag;
        else mag = oldJumpMag;

         playerJumpPow = defJump;
        //�Ē�`
        playerJumpPow = playerJumpPow * (playerManager.DefJumpMag - mag);
        if(!debufJump) debufJump = true;
        else jumpTime = 0.0f;
    }

    //���Ԍv�Z
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

    //�X�e�[�W�\��
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
