using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class RunOnlyPlayerC : MonoBehaviour
{
    [Header("回避用スクリプト"),SerializeField]
    private AvoidanceC avoC;
    [SerializeField]
    private ThirdStageGM thirdGM;

    #region//スピード等のパラメータ関係
    [SerializeField] private Animator anim;
    //[SerializeField] private PlayerInput playerInput;
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

    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
       
        playerSpeed = thirdGM.PlayerSpeed;
        jumpPow = thirdGM.PlayerJumpPow;
    }

    float jumpForce = 10.0f;
    // Update is called once per frame
    void Update()
    {

        //inputMoveVelocity = new Vector2(aa,bb);
        Debug.Log(myPos.y);
       
        if(Input.GetKeyDown(KeyCode.O)) {
            this.rb.AddForce(transform.up * jumpForce);
        }

       // }

        if(!onGround)
        {
            DrowFootRay();
            
        } 
            
            MoveObjects();
       //}
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

  
    Vector3 myPos;
    
    
    bool jump = false;
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
        myPos = this.transform.position;
        //ボタン入力処理
        inputMoveVelocity.x = Input.GetAxis("Horizontal");
        if(inputMoveVelocity.x > 0 && myPos.x <= 6.0f) {
            myPos.x += 0.3f;
            //this.transform.position += new Vector3(inputMoveVelocity.x, inputJumpVelocity, playerSpeed) * playerSpeed;
        }
        else if(inputMoveVelocity.x < 0 && myPos.x >= -6.0f) {
            myPos.x -= 0.3f;
        }
        myPos.z += 0.05f;

        if(Input.GetKeyDown("joystick button 0")) {
            //myRigidbody.useGravity = false;
            if(onGround) {
                jump = true;
                
            }
            //JumpRunPlayer();
        }
        if(jump && myPos.y < 10.0f) {
            //this.rb.AddForce(transform.up * jumpForce);
        }
        else if(myPos.y > 10.0f) {
            jump = false;
        }
        else if(!jump) {
            //this.rb.AddForce(transform.up * jumpForce);
        }
        /*
        if(myPos.y > 10.0f && !onGround) {
            jump = false;
            myPos.y -= 10.0f * Time.deltaTime;
        }
        */
        this.transform.position = myPos;
        // var bb = Input.GetAxis("Vertical");
        //if(inputMoveVelocity.x <= 0.9f && inputMoveVelocity.x >= -0.9f) {
        //if(myPos.x > -7.6f && myPos.x < 8.05f) {
        //_velocity = new Vector3(inputMoveVelocity.x, _rigidbody.velocity.y, playerSpeed).normalized;

        //}

        //}

        //Debug.Log(inputMoveVelocity.x);

        if(!onGround)
        {
            inputJumpVelocity -= _gravity * Time.deltaTime;

            //this.transform.position += new Vector3(inputMoveVelocity.x, inputJumpVelocity , playerSpeed) * playerSpeed;
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
            
            //myRigidbody.useGravity = true;
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
            hitGround = false;
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