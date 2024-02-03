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
    #region//編集可能変数
    [Header("通常の速度"), SerializeField]
    private float playerSpeed;
    [Header("回避時の速度"), SerializeField]
    private float avoidanceSpeed;
    [Header("ダッシュ時の速度"), SerializeField]
    private float dashSpeed;   
    [Header("しゃがみ中の速度"), SerializeField] 
    private float shitSpeed;
    [Header("落下の初速"), SerializeField]
    private float _initFallSpeed;
    [Header("落下時の速さ制限（Infinityで無制限）"), SerializeField]
    private float _fallSpeed;
    [Header("重力加速度"), SerializeField]
    private float _gravity;
    [Header("ジャンプする瞬間の速さ(ジャンプ力)"), SerializeField]
    private float defaultJumpSpeed;
    [Header("ジャンプ台を使った時のジャンプ力"),SerializeField]
    private float addJumpSpeed;
    [Header("スライダーを使った時のジャンプ力"),SerializeField]
    private  float sliderSpeed;

    [Header("ゲージ等を載せたCanvas"),SerializeField]
    private Canvas canvas;

    [Header("ゲーマネ"),SerializeField]
    private GameManager gameManager;
    [Header("アニメーション"),SerializeField]
    private Animator anim = null;
    [Header("エフェクト呼び出す場所のオブジェクト"),SerializeField]
    private GameObject popEffectObject;
    [SerializeField]
    private GameObject shotRayPosition;
    #endregion

    #region//参照するスクリプト
    [SerializeField] private GutsGaugeC gutsGaugeC;
    [SerializeField] private AvoidanceC avoidanceC;
    [SerializeField] private CurrentDebufC debufC;
    private PlayerManager playerManager;
    #endregion

    #region//変数
    //入力されたベクトル
    private Vector2 _inputMove;
    //現在の向きと座標
    private Transform _transform;
    //上向きのベクトル
    private float _verticalVelocity;
    //回転
    private float _turnVelocity;
    //滞空時間
    private float onAirTime = 0.0f;
    //プレイヤーのスピード
    private float _jumpSpeed;
    //雨降っている判定
    private bool isRain = true;
    //接地判定
    private bool _isGroundedPrev;
    //トランポリンの接触判定
    private bool onTramporin = false;
    //ジャンプしたかの判定(落下時に滞空モーションに映る為)
    private bool doJump = false;
    //加速床に乗っている
    private bool onSprite = false;
    private bool doPanelJump = false;

    //スティックの入力
    [SerializeField] private PlayerInput _playerInput;
    private InputAction _buttonAction;
    private CharacterController _characterController;
    #endregion

    #region//enum型
    #endregion

    #region//プロパティ
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

    //ゴール可能状態
    private bool canGoal = false;
    //お守りを所持している状態
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

    //完全なゴール判定フラグ
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
        //縛りの影響を反映させる
        playerSpeed = playerSpeed * playerManager.DefSpeedMag;
        _jumpSpeed = defaultJumpSpeed * playerManager.DefJumpMag;
    }

    float d;
    float P_speed = 15.0f;
    void Update()
    {       
        //滞空時間計算
        if (!_isGroundedPrev) CountOnAir();
        //移動用デバフの時間管理

        #region//関担当
        if(bigJump) {
            anim.SetTrigger("TrnJump");
            _jumpSpeed = addJumpSpeed;
            _fallSpeed = 50f;
            isGrounded = false;
            _verticalVelocity = _jumpSpeed;
            anim.SetTrigger("HighJumpAri");
            _verticalVelocity -= _gravity * Time.deltaTime;

            // 落下する速さ以上にならないように補正
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

                // 落下する速さ以上にならないように補正
                if(_verticalVelocity < -_fallSpeed)
                    _verticalVelocity = -_fallSpeed;
                bigJump = false;
            }

            isGrounded = _characterController.isGrounded;

            if(isGrounded && !_isGroundedPrev) {
                // 着地する瞬間に落下の初速を指定しておく
                _verticalVelocity = -_initFallSpeed;
            } else if(!isGrounded) {
                // 空中にいるときは、下向きに重力加速度を与えて落下させる
                _verticalVelocity -= _gravity * Time.deltaTime;

                // 落下する速さ以上にならないように補正
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
            //プレイヤーの状態管理
            MovePlayer();
        }
    }

    //関担当
    IEnumerator WaitSpeed() {
        yield return new WaitForSeconds(3.0f);
        playerSpeed = 4f;
    }

    //アイテム獲得
    public void GetItem(int getScore, string itemName) {
        //スコア加算
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

    //関担当
    IEnumerator WaitKeyFlag() {
        yield return new WaitForSeconds(3.0f);
        mission.STATMISSION = false;
        mission.YBUTTON = false;
    }

    //体力ゲージの表示がおかしくならない様にするために必要
    private void LateUpdate()
    {
        canvas.transform.LookAt(Camera.main.transform.position);
    }

    #region//ボタン処理
    //移動アクション
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputMove = context.ReadValue<Vector2>();
        //ベクトルの入力
        if(_inputMove != Vector2.zero) anim.SetBool("InputVec", true);
        //停止
        else anim.SetBool("InputVec", false);
    }

    //ジャンプアクション
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
        
        //地面orトランポリンに乗っている時または回避中以外だけ処理する
        if (!context.performed || !_characterController.isGrounded) return;

        //地面でのジャンプ
        if (avoidanceC.AvoiP != AvoidanceC.AvoiPlam.Doing
            && !onTramporin && jumpCout == 0 && !onSprite)
        {
            jumpCout = 1;
            doJump = true;
            anim.SetTrigger("InputJump");
        }

        //トランポリン上のジャンプ力
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

    //回避
    public void OnAvoidance(InputAction.CallbackContext context)
    {
        if(!context.performed || !_characterController.isGrounded) return;
        if(avoidanceC.AvoiP == AvoidanceC.AvoiPlam.CanUse)
        {
            anim.SetTrigger("StartRoll");
            StartAvoidance();
        }
    }
 
    //ダッシュ開始
    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed || 
            !_characterController.isGrounded || 
            gutsGaugeC.GPlam == GutsGaugeC.GutsPlam.CoolTime)
            return;
        StartDash();
    }

    //ダッシュ終了
    public void EndDash(InputAction.CallbackContext context)
    {
        if(!context.performed || gutsGaugeC.GPlam != GutsGaugeC.GutsPlam.Doing) return;
        EndDash();
    }
    #endregion

    #region//行動処理

    //行動処理
    private void MovePlayer()
    {
        _buttonAction = _playerInput.actions.FindAction("Dash");
        _buttonAction = _playerInput.actions.FindAction("Jump");
        var isGrounded = _characterController.isGrounded;

        // 着地する瞬間に落下の初速を指定しておく
        if (isGrounded && !_isGroundedPrev) _verticalVelocity = -_initFallSpeed;

        else if (!isGrounded)
        {
            // 空中にいるときは、下向きに重力加速度を与えて落下させる
            _verticalVelocity -= _gravity * Time.deltaTime;

            // 落下する速さ以上にならないように補正
            if (_verticalVelocity < -_fallSpeed) _verticalVelocity = -_fallSpeed;
        }

        //通常の移動
        if (gutsGaugeC.GPlam != GutsGaugeC.GutsPlam.Doing)
            PlayerMove(playerSpeed * debufC.MoveDebufMag);

        //ダッシュ中
        else if (gutsGaugeC.GPlam == GutsGaugeC.GutsPlam.Doing)
        {
            //スティックの入力があるときのみ処理を行う
            if(_inputMove != Vector2.zero)
                PlayerMove(dashSpeed * debufC.MoveDebufMag);
            //ボタン入力ON、スティック入力NO＝＞スタミナ減少を解除してダッシュを終了する
            else EndDash();
        }

        //回避が入力されたら
        else if(avoidanceC.AvoiP == AvoidanceC.AvoiPlam.Doing 
            && _inputMove != Vector2.zero)
            PlayerMove(avoidanceSpeed * debufC.MoveDebufMag);
        
        _isGroundedPrev = isGrounded;
    }

    //ジャンプ処理
    public void JumpPlayer()
    {
        _verticalVelocity = _jumpSpeed * debufC.JumpDebufMag;
        _characterController.height = 0.5f;
    }

    //トランポリンジャンプ
    public void TrnJumpPlayer() 
        => _verticalVelocity = _jumpSpeed * 2.0f * debufC.JumpDebufMag;

    //


    //移動処理
    private void PlayerMove(float speed)//, Vector3 cameraVec
    {
        //足元の判定
        Vector3 rayPosition = shotRayPosition.transform.position;
        RaycastHit hit;
        //下向きのRayを生成する
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
        //カメラの角度を取得する
        var cameraAngleY = Camera.main.transform.eulerAngles.y;
        Vector3 moveVelocity = new Vector3(
               _inputMove.x * speed,
               _verticalVelocity,
               _inputMove.y * speed
           );

        moveVelocity = Quaternion.Euler(0,cameraAngleY,0) * moveVelocity;
        // 現在フレームの移動量を移動速度から計算
        var moveDelta = moveVelocity * Time.deltaTime;//* cameraVecF 
        Debug.DrawRay(this.transform.position, moveDelta, Color.red, 1.0f);
        //アニメーション
        anim.SetFloat("InputSpeed", _inputMove.magnitude, 0.1f, Time.deltaTime);
        // CharacterControllerに移動量を指定し、オブジェクトを動かす
        _characterController.Move(moveDelta);

        //回転の更新
        if (_inputMove != Vector2.zero)
        {
            // 移動入力がある場合は、振り向き動作も行う
            // 操作入力からy軸周りの目標角度[deg]を計算
            var targetAngleY = 0.0f;
            targetAngleY = -Mathf.Atan2(_inputMove.y, _inputMove.x) * Mathf.Rad2Deg + 90;
            targetAngleY += cameraAngleY;
            // イージングしながら次の回転角度[deg]を計算
            var angleY = Mathf.SmoothDampAngle(
                _transform.eulerAngles.y,
                targetAngleY,
                ref _turnVelocity,
                0.1f
            );

            // オブジェクトの回転を更新
            _transform.rotation = Quaternion.Euler(0, angleY, 0);
        }

    }

    #region//回避判定
    //無敵判定をつける
    public void StartAvoidance()
    {
        avoidanceC.AvoiP = AvoidanceC.AvoiPlam.Doing;
        avoidanceC.UsingAvoidanceGauge();
    }

    //無敵判定を解除する
    public void EndAvoidance()
    {
        avoidanceC.AvoiP = AvoidanceC.AvoiPlam.CoolTime;
        anim.SetTrigger("EndRoll");
    }
    #endregion

    #region//ダッシュ判定
    //ダッシュの開始
    public void StartDash()
    {
        gutsGaugeC.GPlam = GutsGaugeC.GutsPlam.Doing;
        anim.SetBool("OnDash", true);
    }

    //ダッシュの終了
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

    //接触処理
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

        //トランポリン
        if(col.tag == "Tramprin")
        {
            tranporin.SetBool("Tranporin", true);
            isGrounded = false;
            onTramporin = true;
        }

        if(col.tag == "hunn") debufC.ActiveMoveDebuf(60, 0.1f);

        //落とし穴
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
        //地面への着地アニメーションを呼び出す
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

        //ゴール
        if (hit.gameObject.tag == "Goal" && canGoal)
        {
            SousaUIContorller.stageClear++;
            allGoal = true;
            gameManager.NameChange();
        }

        //トラップの関数を取得する
        if(hit.gameObject.tag == "Trap" && avoidanceC.AvoiP != AvoidanceC.AvoiPlam.Doing)
        {
            TrapC trapC = hit.gameObject.GetComponent<TrapC>();
            switch (trapC.DebufKinds)
            {
                //移動用デバフ
                case TrapC.DebufKind.DefMove:
                    debufC.ActiveMoveDebuf(trapC.DebufTime,trapC.DebufMag);
                    break;
                //ジャンプ用デバフ
                case TrapC.DebufKind.DefJump:
                    debufC.ActiveJumpDebuf(trapC.DebufTime, trapC.DebufMag);
                    break;
            }
        }
    }
    #endregion
   
    //滞空時間計算
    private void CountOnAir()
    {
        //ジャンプ入力なし（トランポリンも）で地面から落ちたら
        if(!doJump && !_isGroundedPrev) anim.SetTrigger("OutGround");
        //落下時間を計算
        onAirTime += Time.deltaTime;
    }
}
