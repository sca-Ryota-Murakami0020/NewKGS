// 2021.02 K1Togami Follow Camera
// Base on CameraControllercsv2.1.cs in https://ruhrnuklear.de/fcc/

using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform target; // ターゲット
    private float targetHeight = 5.0f;

    public float distance = 10.0f;
    public float horizontalAngle = 0.0f;
    public float verticalAngle = 10.0f;

    // カメラの移動限界
    public float verticalAngleMinLimit = -30f; // 見上げ限界角度
    public float verticalAngleMaxLimit = 80f; // 見下ろし限界角度 
    public float maxDistance = 20f; // 最大ズーム距離 
    public float minDistance = 0.6f; // 最小ズーム距離 

    //public Vector3 offset = Vector3.zero; // ターゲットとカメラのオフセット

    public float rotationSpeed = 180.0f; // 画面の横幅分カーソルを移動させたとき何度回転するか.
    public float rotationDampening = 0.5f; // 回転の減衰速度 (higher = faster) 
    public float zoomDampening = 5.0f; // Auto Zoom speed (Higher = faster) 

    // 衝突検知用
    public LayerMask collisionLayers = -1; // What the camera will collide with 
    public float offsetFromWall = 0.1f; // 衝突する物体からカメラを遠ざけるときのオフセット 


    private float currentDistance; // 現在のカメラ距離
    private float desiredDistance; // 目標とするカメラ距離
    private float correctedDistance; // 矯正後のカメラ距離

    private float CameraFollowDelay; // カメラ回転後のカメラフォローまでの遅延

    // ユーザによる回転の許可
    public bool allowMouseInput = true; // カメラの方向をマウスでコントロールすることを許可するか。

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        horizontalAngle = angles.x;
        verticalAngle = angles.y;

        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;

        CameraFollowDelay = 0f;
    }

    void LateUpdate()
    {
        // ターゲットが定義されていない場合は何もしない
        if (target == null)
            return;

        Vector3 vTargetOffset; // ターゲットからのオフセット

        if (GUIUtility.hotControl == 0)
        {
            // マウス入力が許可されているかどうかを確認する
            if (allowMouseInput)
            {
                // マウスの右ボタンを押しながらマウスを動かすと、視点を変更できる。
                if (Input.GetMouseButton(1))
                {
                    horizontalAngle += Input.GetAxis("Mouse X") * rotationSpeed * 0.02f;
                    verticalAngle -= Input.GetAxis("Mouse Y") * rotationSpeed * 0.02f;
                    CameraFollowDelay = 1.0f;
                }
            }

            // コントローラの右スティックによる回転を入れる場合は、ここに入れる。
            // 回転速度はお好みに調整してください。
            //var controllerHorizonal = Input.GetAxis("Horizontal_RS");
            //horizontalAngle += controllerHorizonal * rotationSpeed * 0.001f;
            //verticalAngle += Input.GetAxis("Vertical_RS") * rotationSpeed * 0.001f;
            //if (controllerHorizonal != 0f ) CameraFollowDelay = 1.0f;


            if (CameraFollowDelay > 0f)
            {
                CameraFollowDelay -= Time.deltaTime;
            } else
            {
                // マウスによる回転が無効の場合、カメラ視線をターゲットの視線にじわじわあわせる
                RotateBehindTarget();
            }

            verticalAngle = ClampAngle(verticalAngle, verticalAngleMinLimit, verticalAngleMaxLimit);

            // カメラの向きを設定
            Quaternion rotation = Quaternion.Euler( verticalAngle,horizontalAngle, 0);

            // 希望のカメラ位置を計算
            vTargetOffset = new Vector3(0, -targetHeight, 0);
            Vector3 position = target.transform.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset);

            // 高さを使ってユーザーが設定した真のターゲットの希望の登録点を使って衝突をチェック
            RaycastHit collisionHit;
            Vector3 trueTargetPosition = new Vector3(target.transform.position.x,
                target.transform.position.y + targetHeight, target.transform.position.z);

            // 衝突があった場合は、カメラ位置を補正し、補正後の距離を計算
            var isCorrected = false;
            if (Physics.Linecast(trueTargetPosition, position, out collisionHit, collisionLayers))
            {
                // 元の推定位置から衝突位置までの距離を計算し、衝突した物体から安全な「オフセット」距離を差し引く
                // このオフセットは、カメラがヒットした面の真上にいないよう逃がす距離
                correctedDistance = Vector3.Distance(trueTargetPosition, collisionHit.point) - offsetFromWall;
                isCorrected = true;
            }

            // スムージングのために、距離が補正されていないか、または補正された距離が現在の距離より
            // も大きい場合にのみ、距離を返す。
            currentDistance = !isCorrected || correctedDistance > currentDistance
                ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomDampening)
                : correctedDistance;

            // 限界を超えないようにする
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

            // 新しい currentDistance に基づいて位置を再計算する。
            position = target.transform.position - (rotation * Vector3.forward * currentDistance + vTargetOffset);

            // 最後にカメラの回転と位置を設定。
            transform.rotation = rotation;
            transform.position = position;

        }

    }


    // カメラを背後にまわす。
    private void RotateBehindTarget()
    {
        float targetRotationAngle = target.transform.eulerAngles.y;
        float currentRotationAngle = transform.eulerAngles.y;
        horizontalAngle = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);
    }

    // 角度クリッピング
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

}
