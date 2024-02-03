using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Example : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    public GameObject followObject = null;          // 視点となるオブジェクト
    private Vector3 lookPos = Vector3.zero;         // 実際にカメラを向ける座標
    public float lookPlayDistance;                  // 視点の遊び
    public float followSmooth;                      // 追いかけるときの速度
    public float cameraDistance;                    // 視点からカメラまでの距離
    public float cameraHeight;                      // デフォルトのカメラの高さ
    public float currentCameraHeight;               // 現在のカメラの高さ

    public float cameraPlayDiatance;                // 視点からカメラまでの距離の遊び
    public float leaveSmooth;                       // 離れるときの速度

    [SerializeField] private InputActionReference _stickChildAction;    // InputActionAssetへの参照
    private bool isCameraAuto = false;              //右スティックを触っているかどうかのフラグ

    // コールバックの登録・解除
    private void Awake()
    {
        _stickChildAction.action.performed += OnMove;
        _stickChildAction.action.canceled += OnMove;
    }

    private void OnDestroy()
    {
        _stickChildAction.action.performed -= OnMove;
        _stickChildAction.action.canceled -= OnMove;
    }

    // InputActionの有効化・無効化
    private void OnEnable() => _stickChildAction.action.Enable();
    private void OnDisable() => _stickChildAction.action.Disable();

    // コールバックの実装
    private void OnMove(InputAction.CallbackContext context)
    {
        // スティックの子Controlの入力を受け取る
        var childValue = context.ReadValue<float>();

        // 値をログ出力
        Debug.Log($"childValue: {childValue}");
        //右スティックを触っていない
        isCameraAuto = true;
    }

    //右スティックを触っていない時に特定の動作をしたらカメラを自動で調整する
    private void CameraAutoMove()
    {
        if(isCameraAuto)
        {
            //まだ触らない
        }
    }

    void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;
        if (followObject == null) return;

        // ゲームパッドの左右のスティックの入力値を取得
        var x = gamepad.leftStick.x.ReadValue();
        var y = gamepad.leftStick.y.ReadValue();
        var up = gamepad.rightStick.up.ReadValue();
        var down = gamepad.rightStick.down.ReadValue();
        var left = gamepad.rightStick.left.ReadValue();
        var right = gamepad.rightStick.right.ReadValue();

        // 全ての入力をログ出力
        Debug.Log($"x: {x}, y: {y}, up: {up}, down: {down}, left: {left}, right: {right}");
        
        // ゲームパッドの左右のスティックの入力値を取得
        var rightStick = gamepad.rightStick.ReadValue();
        var leftStick = gamepad.leftStick.ReadValue();
        
        float rotationX = rightStick.x * followSmooth;
        float rotationY = rightStick.y * followSmooth;
        CameraMove(rotationX,rotationY);

        transform.LookAt(lookPos);
    }

    //右スティックでカメラの動きを操作する
    void CameraMove(float x,float y)
    {
        Vector3 rotation = transform.eulerAngles;

        rotation.y += x;
        rotation.x -= y;

        // 垂直方向の回転を一定範囲内に制限する（例えば、-90度から90度の間）
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);

        // カメラの回転を適用する
        transform.eulerAngles = rotation;
    }
}