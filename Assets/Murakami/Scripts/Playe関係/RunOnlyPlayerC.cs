using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunOnlyPlayerC : MonoBehaviour
{
    [Header("回避用スクリプト"),SerializeField]
    private AvoidanceC avoC;
    [SerializeField]
    private ThirdStageGM thirdGM;

    #region//スピード等のパラメータ関係
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float _gravity;
    [SerializeField] private float inputFallSpeed;
    [SerializeField] private float fallSpeed;
    //Rayを飛ばすポジション
    [SerializeField] private GameObject shotRayPosition;

    private InputAction buttonAction;
    private float playerSpeed = 0.0f;
    private float jumpPow = 0.0f;
    private float inputJumpVelocity = 0.0f;
    private int jumpCount = 0;
    private bool onGround = false;
    private bool hitGround = false;

    private Vector2 inputMoveVelocity = Vector2.zero;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = thirdGM.PlayerSpeed;
        jumpPow = thirdGM.PlayerJumpPow;
    }

    // Update is called once per frame
    void Update()
    {

        //inputMoveVelocity = new Vector2(aa,bb);
        //Debug.Log(inputMoveVelocity);
        Debug.Log(onGround);
        MoveObjects();
        if(!onGround)
        {
            DrowFootRay();
        }
    }

    //移動入力処理
    /*
    public void OnMove(InputAction.CallbackContext context)
    {      
        inputMoveVelocity = context.ReadValue<Vector2>();
    }*/

    public void OnMove(InputValue c)
    {
         Debug.Log("yonda");
        inputMoveVelocity = c.Get<Vector2>();
        if(inputMoveVelocity == Vector2.zero) return;
    }

    //ジャンプ入力処理
    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed || jumpCount != 0)
            return;
        if(avoC.AvoiP != AvoidanceC.AvoiPlam.Doing
            && jumpCount == 0)
        {
            jumpCount += 1;
            anim.SetTrigger("DoJump");
        }
    }

    //回避入力処理
    public void OnAvoidance(InputAction.CallbackContext context)
    {
        if(!context.performed || !onGround) return;
        if(avoC.AvoiP == AvoidanceC.AvoiPlam.CanUse)
        {
            anim.SetTrigger("StartRoll");
            StartAvoidance();
        }
    }

    //回避判定の取得
    public void StartAvoidance()
    {
        avoC.AvoiP = AvoidanceC.AvoiPlam.Doing;
        avoC.UsingAvoidanceGauge();
    }
    //回避判定の解除
    public void EndAvoidance()
    {
        avoC.AvoiP = AvoidanceC.AvoiPlam.CoolTime;
        anim.SetTrigger("EndRoll");
    }

    //ジャンプ処理
    private void JumpRunPlayer()
    {
        inputJumpVelocity = jumpPow;
    }

    //プレイヤーの前進処理
    private void MoveObjects()
    {
        //buttonAction = playerInput.actions.FindAction("Move");
        /*
        if(onGround && !hitGround)
        {
            inputJumpVelocity = -inputFallSpeed;
            Debug.Log("初速設定");
        }
        if(!onGround)
        {
            inputJumpVelocity = -_gravity * Time.deltaTime;
            if(inputJumpVelocity < -fallSpeed) inputJumpVelocity = -fallSpeed;
            Debug.Log("落下");
        }*/

        //ボタン入力処理
        inputMoveVelocity.x = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown("joystick button 0"))
        {
            JumpRunPlayer();
        }
       // var bb = Input.GetAxis("Vertical");
        this.transform.position += new Vector3(inputMoveVelocity.x, inputJumpVelocity, playerSpeed) * playerSpeed;
        //Debug.Log(inputMoveVelocity.x);

        if(!onGround && !hitGround)
        {
            inputJumpVelocity -= _gravity * Time.deltaTime;
            if(inputJumpVelocity <= 0.0f) inputJumpVelocity = 0.0f;
        }
    }

    //接触判定
    private void OnCollisionEnter(Collision collision)
    {
        //地面
        if(collision.gameObject.tag == "Ground")
        {
            onGround = true;
            hitGround = true;

        }           
        //何かしらのギミックに引っかかった時
        if(collision.gameObject.tag == "Trap")
        {

        }
    }

    //離れた判定
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {

            onGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Goal")
        {
            //Debug.Log("omedetou");
            thirdGM.StageClear();

        }
    }

    //足元からRayを飛ばす
    private void DrowFootRay()
    {
        Vector3 rayPosition = shotRayPosition.transform.position;
        RaycastHit hit;
        //下向きのRayを生成する
        Ray ray = new Ray(rayPosition,-this.gameObject.transform.up);
        Debug.DrawRay(shotRayPosition.transform.position, -shotRayPosition.transform.up);
        //float distance = Vector3.Distance(hit, rayPosition);
        if(!Physics.Raycast(ray, out hit, 1.2f))
        {
            hitGround = false;
            Debug.Log("落ちろ");
            inputJumpVelocity = 0.0f;
        }
        if(Physics.Raycast(ray, out hit, 0.06f))
        {
            Debug.Log("uuuuuuuu");
        }
    }
}