using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;
using PathCreation;

[RequireComponent(typeof(CharacterController))]
public class PlayerC : MonoBehaviour
{
    #region//�ҏW�\�ϐ�
    [Header("�ʏ�̑��x"), SerializeField]
    private float playerSpeed;
    [Header("������̑��x"), SerializeField]
    private float avoidanceSpeed;
    [Header("�_�b�V�����̑��x"), SerializeField]
    private float dashSpeed;   
    [Header("���Ⴊ�ݒ��̑��x"), SerializeField] 
    private float shitSpeed;
    [Header("�����̏���"), SerializeField]
    private float _initFallSpeed;
    [Header("�������̑��������iInfinity�Ŗ������j"), SerializeField]
    private float _fallSpeed;
    [Header("�d�͉����x"), SerializeField]
    private float _gravity;
    [Header("�W�����v����u�Ԃ̑���(�W�����v��)"), SerializeField]
    private float defaultJumpSpeed;
    [Header("�W�����v����g�������̃W�����v��"),SerializeField]
    private float addJumpSpeed;
    [Header("�X���C�_�[���g�������̃W�����v��"),SerializeField]
    private  float sliderSpeed;

    [Header("�Q�[�W�����ڂ���Canvas"),SerializeField]
    private Canvas canvas;

    [Header("�Q�[�}�l"),SerializeField]
    private GameManager gameManager;
    [Header("�A�j���[�V����"),SerializeField]
    private Animator anim = null;
    [Header("�G�t�F�N�g�Ăяo���ꏊ�̃I�u�W�F�N�g"),SerializeField]
    private GameObject popEffectObject;
    [SerializeField]
    private GameObject shotRayPosition;
    #endregion

    #region//�Q�Ƃ���X�N���v�g
    [SerializeField] private GutsGaugeC gutsGaugeC;
    [SerializeField] private AvoidanceC avoidanceC;
    [SerializeField] private CurrentDebufC debufC;
    private PlayerManager playerManager;
    #endregion

    #region//�ϐ�
    //���͂��ꂽ�x�N�g��
    private Vector2 _inputMove;
    //���݂̌����ƍ��W
    private Transform _transform;
    //������̃x�N�g��
    private float _verticalVelocity;
    //��]
    private float _turnVelocity;
    //�؋󎞊�
    private float onAirTime = 0.0f;
    //�v���C���[�̃X�s�[�h
    private float _jumpSpeed;
    //�J�~���Ă��锻��
    private bool isRain = true;
    //�ڒn����
    private bool _isGroundedPrev;
    //�g�����|�����̐ڐG����
    private bool onTramporin = false;
    //�W�����v�������̔���(�������ɑ؋󃂁[�V�����ɉf���)
    private bool doJump = false;
    //�������ɏ���Ă���
    private bool onSprite = false;
    private bool doPanelJump = false;

    //�X�e�B�b�N�̓���
    [SerializeField] private PlayerInput _playerInput;
    private InputAction _buttonAction;
    private CharacterController _characterController;
    #endregion

    #region//enum�^
    #endregion

    #region//�v���p�e�B
    public bool IsRain
    {
        get { return this.isRain;}
        set { this.isRain = value;}
    }

    public bool IsGrand
    {
        get { return this._isGroundedPrev;}
    }

    public GameObject PopObject
    {
        get { return this.popEffectObject;}
    }
    #endregion

    //�S�[���\���
    private bool canGoal = false;
    //�������������Ă�����
    private bool getedGuardian = false;
    public bool GetedGuardian {
        get { return this.getedGuardian; }
        set { this.getedGuardian = value; }
    }

    public bool CanGoal {
        get { return this.canGoal; }
    }

    [SerializeField] MeshCollider planeCol;


    [SerializeField] PathCreator Path;

    [SerializeField] MissionManager mission;

    bool missio = false;
    public bool MISSIO {
        set {
            missio = value;
        }
        get {
            return missio;
        }
    }
    float missioTime;

    [SerializeField] Animator tranporin;
    [SerializeField] GameObject DamagePanel;

    //���S�ȃS�[������t���O
    bool allGoal = false;

    public bool ALLGOAL {
        set {
            this.allGoal = value;
        }
        get {
            return this.allGoal;
        }
    }

    bool falling = false;
    public bool FALLING {
        set {
            this.falling = value;
        }
        get {
            return falling;
        }
    }

    int KeyCount = 0;
    public int KEYCOUNT {
        set {
            this.KeyCount = value;
        }
        get {
            return this.KeyCount;
        }
    }
    bool silde = false;
    bool isGrounded;
    bool bigJump = false;
    bool getKey = false;
    bool slideFlag = false;
    public bool GETKEY {
        set {
            this.getKey = value; ;
        }
        get {
            return this.getKey;
        }
    }

    [SerializeField] GameObject slopeObj;
    Animator myAnim;
    private void Awake()
    {
        myAnim = this.GetComponent<Animator>();
        myAnim.enabled = true;
        mission = mission.GetComponent<MissionManager>();
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        DamagePanel.SetActive(false);       
        planeCol.enabled = true;

        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        //����̉e���𔽉f������
        playerSpeed = playerSpeed * playerManager.DefSpeedMag;
        _jumpSpeed = defaultJumpSpeed * playerManager.DefJumpMag;
    }

    float d;
    float P_speed = 15.0f;
    void Update()
    {       
        //�؋󎞊Ԍv�Z
        if (!_isGroundedPrev) CountOnAir();
        //�ړ��p�f�o�t�̎��ԊǗ�

        #region//�֒S��
        if(bigJump) {
            anim.SetTrigger("TrnJump");
            _jumpSpeed = addJumpSpeed;
            _fallSpeed = 50f;
            isGrounded = false;
            _verticalVelocity = _jumpSpeed;
            anim.SetTrigger("HighJumpAri");
            _verticalVelocity -= _gravity * Time.deltaTime;

            // �������鑬���ȏ�ɂȂ�Ȃ��悤�ɕ␳
            if(_verticalVelocity < -_fallSpeed)
                _verticalVelocity = -_fallSpeed;
            bigJump = false;
        } else {
            _fallSpeed = 30f;
        }
            if (onTramporin) {
            bigJump = true;

            if(bigJump) {
                anim.SetTrigger("TrnJump");
                _jumpSpeed = addJumpSpeed;
                _fallSpeed = 50f;
                isGrounded = false;
                _verticalVelocity = _jumpSpeed;
                anim.SetTrigger("HighJumpAri");
                _verticalVelocity -= _gravity * Time.deltaTime;

                // �������鑬���ȏ�ɂȂ�Ȃ��悤�ɕ␳
                if(_verticalVelocity < -_fallSpeed)
                    _verticalVelocity = -_fallSpeed;
                bigJump = false;
            }

            isGrounded = _characterController.isGrounded;

            if(isGrounded && !_isGroundedPrev) {
                // ���n����u�Ԃɗ����̏������w�肵�Ă���
                _verticalVelocity = -_initFallSpeed;
            } else if(!isGrounded) {
                // �󒆂ɂ���Ƃ��́A�������ɏd�͉����x��^���ė���������
                _verticalVelocity -= _gravity * Time.deltaTime;

                // �������鑬���ȏ�ɂȂ�Ȃ��悤�ɕ␳
                if(_verticalVelocity < -_fallSpeed)
                    _verticalVelocity = -_fallSpeed;
            }

            _isGroundedPrev = isGrounded;
        }

        if(_characterController.isGrounded && silde) {
            playerSpeed = 6f;
            StartCoroutine(WaitSpeed());
        }

        if(missio) {
            missioTime += Time.deltaTime;
        }
        if(missioTime >= 3.0f) {
            mission.MiSSIONCOUNT++;
            missio = false;
            missioTime = 0;
        }
        if(allGoal && !gameManager.GAMEOVER) {
            _playerInput.enabled = false;

        }
        #endregion
        

        else if(slideFlag) {
            d += P_speed * Time.deltaTime;
            _transform.position = Path.path.GetPointAtDistance(d);
            
        } else {
            //�v���C���[�̏�ԊǗ�
            MovePlayer();
        }
    }

    //�֒S��
    IEnumerator WaitSpeed() {
        yield return new WaitForSeconds(3.0f);
        playerSpeed = 4f;
    }

    //�A�C�e���l��
    public void GetItem(int getScore, string itemName) {
        //�X�R�A���Z
        gameManager.AddScore(getScore);
        switch(itemName) {
            case "Key":
                getKey = true;
                if(mission.MiSSIONCOUNT < 2) {
                    StartCoroutine(WaitKeyFlag());
                }
                if(KeyCount < 4) {
                    KeyCount++;

                } else {

                    KeyCount = 0;
                }

                if(KeyCount == 3) {
                    canGoal = true;
                }
                break;
            case "Gard":
                getedGuardian = true;
                break;
        }
    }

    //�֒S��
    IEnumerator WaitKeyFlag() {
        yield return new WaitForSeconds(3.0f);
        mission.STATMISSION = false;
        mission.YBUTTON = false;
    }

    //�̗̓Q�[�W�̕\�������������Ȃ�Ȃ��l�ɂ��邽�߂ɕK�v
    private void LateUpdate()
    {
        canvas.transform.LookAt(Camera.main.transform.position);
    }

    #region//�{�^������
    //�ړ��A�N�V����
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputMove = context.ReadValue<Vector2>();
        //�x�N�g���̓���
        if(_inputMove != Vector2.zero) anim.SetBool("InputVec", true);
        //��~
        else anim.SetBool("InputVec", false);
    }

    //�W�����v�A�N�V����
    public void OnJump(InputAction.CallbackContext context)
    {
        
        if(silde)
        {
            anim.SetTrigger("TrnJump");
            DOTween.KillAll();
            _jumpSpeed = sliderSpeed;
            playerSpeed = 10.0f;
            _verticalVelocity = _jumpSpeed;
            
            
            silde = false;
        }
        else _jumpSpeed = defaultJumpSpeed;
        
        //�n��or�g�����|�����ɏ���Ă��鎞�܂��͉�𒆈ȊO������������
        if (!context.performed || !_characterController.isGrounded) return;

        //�n�ʂł̃W�����v
        if (avoidanceC.AvoiP != AvoidanceC.AvoiPlam.Doing
            && !onTramporin && jumpCout == 0 && !onSprite)
        {
            jumpCout = 1;
            doJump = true;
            anim.SetTrigger("InputJump");
        }

        //�g�����|������̃W�����v��
        if (onTramporin)
        {
            doJump = true;
            anim.SetTrigger("TrnJump");
        }

        if(onSprite)
        {
            doJump = true;
            doPanelJump  =true;
            //_verticalVelocity = 50.0f;
            anim.SetTrigger("PanelJump");
        }
    }

    //���
    public void OnAvoidance(InputAction.CallbackContext context)
    {
        if(!context.performed || !_characterController.isGrounded) return;
        if(avoidanceC.AvoiP == AvoidanceC.AvoiPlam.CanUse)
        {
            anim.SetTrigger("StartRoll");
            StartAvoidance();
        }
    }
 
    //�_�b�V���J�n
    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed || 
            !_characterController.isGrounded || 
            gutsGaugeC.GPlam == GutsGaugeC.GutsPlam.CoolTime)
            return;
        StartDash();
    }

    //�_�b�V���I��
    public void EndDash(InputAction.CallbackContext context)
    {
        if(!context.performed || gutsGaugeC.GPlam != GutsGaugeC.GutsPlam.Doing) return;
        EndDash();
    }
    #endregion

    #region//�s������

    //�s������
    private void MovePlayer()
    {
        _buttonAction = _playerInput.actions.FindAction("Dash");
        _buttonAction = _playerInput.actions.FindAction("Jump");
        var isGrounded = _characterController.isGrounded;

        // ���n����u�Ԃɗ����̏������w�肵�Ă���
        if (isGrounded && !_isGroundedPrev) _verticalVelocity = -_initFallSpeed;

        else if (!isGrounded)
        {
            // �󒆂ɂ���Ƃ��́A�������ɏd�͉����x��^���ė���������
            _verticalVelocity -= _gravity * Time.deltaTime;

            // �������鑬���ȏ�ɂȂ�Ȃ��悤�ɕ␳
            if (_verticalVelocity < -_fallSpeed) _verticalVelocity = -_fallSpeed;
        }

        //�ʏ�̈ړ�
        if (gutsGaugeC.GPlam != GutsGaugeC.GutsPlam.Doing)
            PlayerMove(playerSpeed * debufC.MoveDebufMag);

        //�_�b�V����
        else if (gutsGaugeC.GPlam == GutsGaugeC.GutsPlam.Doing)
        {
            //�X�e�B�b�N�̓��͂�����Ƃ��̂ݏ������s��
            if(_inputMove != Vector2.zero)
                PlayerMove(dashSpeed * debufC.MoveDebufMag);
            //�{�^������ON�A�X�e�B�b�N����NO�����X�^�~�i�������������ă_�b�V�����I������
            else EndDash();
        }

        //��������͂��ꂽ��
        else if(avoidanceC.AvoiP == AvoidanceC.AvoiPlam.Doing 
            && _inputMove != Vector2.zero)
            PlayerMove(avoidanceSpeed * debufC.MoveDebufMag);
        
        _isGroundedPrev = isGrounded;
    }

    //�W�����v����
    public void JumpPlayer()
    {
        _verticalVelocity = _jumpSpeed * debufC.JumpDebufMag;
        _characterController.height = 0.5f;
    }

    //�g�����|�����W�����v
    public void TrnJumpPlayer() 
        => _verticalVelocity = _jumpSpeed * 2.0f * debufC.JumpDebufMag;

    //


    //�ړ�����
    private void PlayerMove(float speed)//, Vector3 cameraVec
    {
        //�����̔���
        Vector3 rayPosition = shotRayPosition.transform.position;
        RaycastHit hit;
        //��������Ray�𐶐�����
        Ray ray = new Ray(rayPosition, -this.gameObject.transform.up);

        //float distance = Vector3.Distance(hit, rayPosition);
        if (!Physics.Raycast(ray, out hit, 1.2f) && _isGroundedPrev)
        {
            onSprite = false;
        }
        if (Physics.Raycast(ray, out hit, 0.06f) && _isGroundedPrev)
        {
            onSprite = true;

        }
        //�J�����̊p�x���擾����
        var cameraAngleY = Camera.main.transform.eulerAngles.y;
        Vector3 moveVelocity = new Vector3(
               _inputMove.x * speed,
               _verticalVelocity,
               _inputMove.y * speed
           );

        moveVelocity = Quaternion.Euler(0,cameraAngleY,0) * moveVelocity;
        // ���݃t���[���̈ړ��ʂ��ړ����x����v�Z
        var moveDelta = moveVelocity * Time.deltaTime;//* cameraVecF 
        Debug.DrawRay(this.transform.position, moveDelta, Color.red, 1.0f);
        //�A�j���[�V����
        anim.SetFloat("InputSpeed", _inputMove.magnitude, 0.1f, Time.deltaTime);
        // CharacterController�Ɉړ��ʂ��w�肵�A�I�u�W�F�N�g�𓮂���
        _characterController.Move(moveDelta);

        //��]�̍X�V
        if (_inputMove != Vector2.zero)
        {
            // �ړ����͂�����ꍇ�́A�U�����������s��
            // ������͂���y������̖ڕW�p�x[deg]���v�Z
            var targetAngleY = 0.0f;
            targetAngleY = -Mathf.Atan2(_inputMove.y, _inputMove.x) * Mathf.Rad2Deg + 90;
            targetAngleY += cameraAngleY;
            // �C�[�W���O���Ȃ��玟�̉�]�p�x[deg]���v�Z
            var angleY = Mathf.SmoothDampAngle(
                _transform.eulerAngles.y,
                targetAngleY,
                ref _turnVelocity,
                0.1f
            );

            // �I�u�W�F�N�g�̉�]���X�V
            _transform.rotation = Quaternion.Euler(0, angleY, 0);
        }

    }

    #region//��𔻒�
    //���G���������
    public void StartAvoidance()
    {
        avoidanceC.AvoiP = AvoidanceC.AvoiPlam.Doing;
        avoidanceC.UsingAvoidanceGauge();
    }

    //���G�������������
    public void EndAvoidance()
    {
        avoidanceC.AvoiP = AvoidanceC.AvoiPlam.CoolTime;
        anim.SetTrigger("EndRoll");
    }
    #endregion

    #region//�_�b�V������
    //�_�b�V���̊J�n
    public void StartDash()
    {
        gutsGaugeC.GPlam = GutsGaugeC.GutsPlam.Doing;
        anim.SetBool("OnDash", true);
    }

    //�_�b�V���̏I��
    public void EndDash()
    {
        gutsGaugeC.JugeStamina();
        anim.SetBool("OnDash",false);
    }
    #endregion

    public void CheckLanding()
    {

    }
    int jumpCout = 0;
    
    int co = 0;

    //�ڐG����
    private void OnTriggerEnter(Collider col)
    {
        
        if(col.tag == "Ice") {
            slideFlag = true;
            _playerInput.enabled = false;
            myAnim.enabled = false;
            slopeObj.SetActive(true);
            this.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            /*
            silde = true;
            for(int u = 0; u < kaidan.Length; u++) {
                kaidan[u].enabled = false;
            }
             */
        }

        //�g�����|����
        if(col.tag == "Tramprin")
        {
            tranporin.SetBool("Tranporin", true);
            isGrounded = false;
            onTramporin = true;
        }

        if(col.tag == "hunn") debufC.ActiveMoveDebuf(60, 0.1f);

        //���Ƃ���
        if (col.tag == "holl")
        {
            falling = true;
            
            planeCol.enabled = false;
            
            _playerInput.enabled = false;
            StartCoroutine(WaitChara());
        }

        if(col.tag == "mission") {

            mission.KeyActive(mission.RADOMMISSIONCOUNT);
            if(mission.RADOMMISSIONCOUNT != 2) {
                mission.MISSIONVALUE[mission.RADOMMISSIONCOUNT]++;
            }
            if(mission.MiSSIONCOUNT != 3) {
                missio = true;
            }

            col.gameObject.SetActive(false);
        }

        if(col.tag == "Enemy") {
            co = 1;
            StartCoroutine(WaitFall());
            _playerInput.enabled = false;
            StartCoroutine(WaitChara());
        }

        if(col.tag == "Car") {
            if(gameManager.CurrentRemain != 0) {
                falling = true;
                _playerInput.enabled = false;
            }
        }

        if(col.tag == "slope") {
            myAnim.enabled = true;
            _playerInput.enabled = true;
            slopeObj.SetActive(false);
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            slideFlag = false;
            //if(Gamepad.current.aButton.wasPressedThisFrame) {
                //bigJump = true;
            //}
           
        }
        if(col.tag == "SpritePanel")
            onSprite = true;
    }

    IEnumerator WaitChara() {
        yield return new WaitForSeconds(1.0f);
        _characterController.enabled = false;
    }

    IEnumerator WaitFall() {
        yield return null;

        while(co != 0) {

            DamagePanel.SetActive(false);

            yield return new WaitForSeconds(0.15f);

            DamagePanel.SetActive(true);

            yield return new WaitForSeconds(0.15f);
            co--;
        }
        DamagePanel.SetActive(false);
        falling = true;
        yield break;
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.tag == "Tramprin") {
            StartCoroutine(WaitJump());
            onTramporin = false;
        }
    }

    IEnumerator WaitJump() {
        yield return new WaitForSeconds(1f);
        tranporin.SetBool("Tranporin", false);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //�n�ʂւ̒��n�A�j���[�V�������Ăяo��
        if(hit.gameObject.tag == "Ground")
        {
            _characterController.height = 0.95f;
            if(!_characterController.isGrounded) {
                jumpCout = 0;
            }
            if(onAirTime <= 2.0f || doPanelJump)
            anim.SetTrigger("Landing");
            else
            anim.SetTrigger("HighLanding");
            onAirTime = 0.0f;
            doJump = false;
        }

        //�S�[��
        if (hit.gameObject.tag == "Goal" && canGoal)
        {
            SousaUIContorller.stageClear++;
            allGoal = true;
            gameManager.NameChange();
        }

        //�g���b�v�̊֐����擾����
        if(hit.gameObject.tag == "Trap" && avoidanceC.AvoiP != AvoidanceC.AvoiPlam.Doing)
        {
            TrapC trapC = hit.gameObject.GetComponent<TrapC>();
            switch (trapC.DebufKinds)
            {
                //�ړ��p�f�o�t
                case TrapC.DebufKind.DefMove:
                    debufC.ActiveMoveDebuf(trapC.DebufTime,trapC.DebufMag);
                    break;
                //�W�����v�p�f�o�t
                case TrapC.DebufKind.DefJump:
                    debufC.ActiveJumpDebuf(trapC.DebufTime, trapC.DebufMag);
                    break;
            }
        }
    }
    #endregion
   
    //�؋󎞊Ԍv�Z
    private void CountOnAir()
    {
        //�W�����v���͂Ȃ��i�g�����|�������j�Œn�ʂ��痎������
        if(!doJump && !_isGroundedPrev) anim.SetTrigger("OutGround");
        //�������Ԃ��v�Z
        onAirTime += Time.deltaTime;
    }
}
