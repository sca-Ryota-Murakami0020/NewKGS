using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunOnlyPlayerC : MonoBehaviour
{
    [Header("���p�X�N���v�g"),SerializeField]
    private AvoidanceC avoC;
    [SerializeField]
    private ThirdStageGM thirdGM;

    #region//�X�s�[�h���̃p�����[�^�֌W
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float _gravity;
    [SerializeField] private float inputFallSpeed;
    [SerializeField] private float fallSpeed;
    //Ray���΂��|�W�V����
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

    //�ړ����͏���
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

    //�W�����v���͏���
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

    //�����͏���
    public void OnAvoidance(InputAction.CallbackContext context)
    {
        if(!context.performed || !onGround) return;
        if(avoC.AvoiP == AvoidanceC.AvoiPlam.CanUse)
        {
            anim.SetTrigger("StartRoll");
            StartAvoidance();
        }
    }

    //��𔻒�̎擾
    public void StartAvoidance()
    {
        avoC.AvoiP = AvoidanceC.AvoiPlam.Doing;
        avoC.UsingAvoidanceGauge();
    }
    //��𔻒�̉���
    public void EndAvoidance()
    {
        avoC.AvoiP = AvoidanceC.AvoiPlam.CoolTime;
        anim.SetTrigger("EndRoll");
    }

    //�W�����v����
    private void JumpRunPlayer()
    {
        inputJumpVelocity = jumpPow;
    }

    //�v���C���[�̑O�i����
    private void MoveObjects()
    {
        //buttonAction = playerInput.actions.FindAction("Move");
        /*
        if(onGround && !hitGround)
        {
            inputJumpVelocity = -inputFallSpeed;
            Debug.Log("�����ݒ�");
        }
        if(!onGround)
        {
            inputJumpVelocity = -_gravity * Time.deltaTime;
            if(inputJumpVelocity < -fallSpeed) inputJumpVelocity = -fallSpeed;
            Debug.Log("����");
        }*/

        //�{�^�����͏���
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

    //�ڐG����
    private void OnCollisionEnter(Collision collision)
    {
        //�n��
        if(collision.gameObject.tag == "Ground")
        {
            onGround = true;
            hitGround = true;

        }           
        //��������̃M�~�b�N�Ɉ�������������
        if(collision.gameObject.tag == "Trap")
        {

        }
    }

    //���ꂽ����
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

    //��������Ray���΂�
    private void DrowFootRay()
    {
        Vector3 rayPosition = shotRayPosition.transform.position;
        RaycastHit hit;
        //��������Ray�𐶐�����
        Ray ray = new Ray(rayPosition,-this.gameObject.transform.up);
        Debug.DrawRay(shotRayPosition.transform.position, -shotRayPosition.transform.up);
        //float distance = Vector3.Distance(hit, rayPosition);
        if(!Physics.Raycast(ray, out hit, 1.2f))
        {
            hitGround = false;
            Debug.Log("������");
            inputJumpVelocity = 0.0f;
        }
        if(Physics.Raycast(ray, out hit, 0.06f))
        {
            Debug.Log("uuuuuuuu");
        }
    }
}