using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameCamara : MonoBehaviour
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

    // InputActionAssetへの参照
    [SerializeField] private InputActionReference _moveAction;

    // コールバックの登録・解除
    private void Awake()
    {
        // 入力値が0以外の値に変化したときに呼び出されるコールバック
        _moveAction.action.performed += OnMove;

        // 入力値が0に戻ったときに呼び出されるコールバック
        _moveAction.action.canceled += OnMove;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (followObject == null) return;

        //UpdateLookPosition();
        //UpdateCameraPosition();
        CameraMove();

        transform.LookAt(lookPos);
    }

    private void OnDestroy()
    {
        _moveAction.action.performed -= OnMove;
        _moveAction.action.canceled -= OnMove;
    }

    // InputActionの有効化・無効化
    private void OnEnable() => _moveAction.action.Enable();
    private void OnDisable() => _moveAction.action.Disable();

    // コールバックの実装
    private void OnMove(InputAction.CallbackContext context)
    {
        // 2軸入力を受け取る
        var move = context.ReadValue<Vector2>();

        // 2軸入力の値を表示
        Debug.Log($"move: {move}");
    }

    /*void UpdateLookPosition()
    {
        // 目標の視点と現在の視点の距離を求める
        Vector3 vec = followObject.transform.position - lookPos;
        float distance = vec.magnitude;

        if (distance > lookPlayDistance)
        {   // 遊びの距離を超えていたら目標の視点に近づける
            float move_distance = (distance - lookPlayDistance) * (Time.deltaTime * followSmooth);
            lookPos += vec.normalized * move_distance;
        }
    }

    void UpdateCameraPosition()
    {
        // XZ平面におけるカメラと視点の距離を取得する
        Vector3 xz_vec = followObject.transform.position - transform.position;
        xz_vec.y = 0;
        float distance = xz_vec.magnitude;

        // カメラの移動距離を求める
        float move_distance = 0;
        if (distance > cameraDistance + cameraPlayDiatance)
        {   // カメラが遊びを超えて離れたら追いかける
            move_distance = distance - (cameraDistance + cameraPlayDiatance);
            move_distance *= Time.deltaTime * followSmooth;
        }
        else if (distance < cameraDistance - cameraPlayDiatance)
        {   // カメラが遊びを超えて近づいたら離れる
            move_distance = distance - (cameraDistance - cameraPlayDiatance);
            move_distance *= Time.deltaTime * leaveSmooth;
        }

        // 新しいカメラの位置を求める
        Vector3 camera_pos = transform.position + (xz_vec.normalized * move_distance);

        // 高さは常に現在の視点からの一定の高さを維持する
        camera_pos.y = lookPos.y + currentCameraHeight;

        transform.position = camera_pos;
    }*/

    void CameraMove()
    {
        // ゲームパッド（デバイス取得）
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        // ゲームパッドの左右のスティックの入力値を取得
        var x = gamepad.leftStick.x.ReadValue();
        var y = gamepad.leftStick.y.ReadValue();
        var up = gamepad.rightStick.up.ReadValue();
        var down = gamepad.rightStick.down.ReadValue();
        var left = gamepad.rightStick.left.ReadValue();
        var right = gamepad.rightStick.right.ReadValue();

        // 全ての入力をログ出力
        print($"x: {x}, y: {y}, up: {up}, down: {down}, left: {left}, right: {right}");


        // ゲームパッドの右のスティックの入力値を取得
        var rightStick = gamepad.rightStick.ReadValue();



        transform.LookAt(lookPos);

    }

}
